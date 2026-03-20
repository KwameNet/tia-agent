# TIA Portal V20 Openness API — Compatibility Fixes

This document covers all fixes applied to `TiaMcpServer` to support TIA Portal V20's Openness API, which introduced several breaking changes from V15–V19.

---

## 1. SCL Import: SimaticML XML → ExternalSourceGroup

### Problem

The original `ImportSclBlock` wrote raw SCL to a `.scl` temp file and called `PlcBlockComposition.Import()`. V20's `Import()` API requires SimaticML XML, not raw SCL.

A second attempt wrapped the SCL in SimaticML XML using a custom `BuildSimaticMlFromScl()` method. This failed at runtime with:

```
The element 'StructuredText' in namespace
'http://www.siemens.com/automation/Openness/SW/NetworkSource/StructuredText/v2'
has invalid child element 'Code' ...
List of possible elements expected: 'Access, Token, Parameter, Text, Comment,
LineComment, Blank, NewLine'
```

V20's `StructuredText/v2` schema does not accept a `<Code>` element — it requires the SCL body to be fully tokenized into individual AST elements (`Access`, `Token`, `Parameter`, etc.). Building a tokenized AST from raw SCL is impractical.

### Fix

Replaced the `PlcBlockComposition.Import()` path with the `ExternalSourceGroup` API, which lets TIA Portal's own SCL compiler handle parsing:

```csharp
// 1. Write raw SCL to a temp .scl file
var tempFile = Path.Combine(Path.GetTempPath(), $"tia_{Guid.NewGuid():N}.scl");
File.WriteAllText(tempFile, sclCode, Encoding.UTF8);

// 2. Delete existing block (V20 does NOT auto-overwrite — see Fix 1a below)
var match = Regex.Match(sclCode,
    @"(?:FUNCTION|FUNCTION_BLOCK|DATA_BLOCK|ORGANIZATION_BLOCK)\s+""([^""]+)""",
    RegexOptions.IgnoreCase);
if (match.Success)
{
    var existing = FindBlock(plcSw.BlockGroup, match.Groups[1].Value);
    existing?.Delete();
}

// 3. Register as an external source
dynamic extSrcFiles = plcSw.ExternalSourceGroup.ExternalSourceFiles;
dynamic extSrc = extSrcFiles.CreateFromFile(new FileInfo(tempFile));

// 4. TIA's SCL compiler generates the block
try   { extSrc.GenerateBlocksFromSource(); }
finally { try { extSrc.Delete(); } catch { } }
```

The `blockGroup` parameter is no longer used for SCL imports.

### Key Takeaway

SimaticML XML import is only viable for LAD/FBD blocks or if you build a full SCL tokenizer. **For SCL, always use the ExternalSourceGroup API.**

---

## 1a. ExternalSourceGroup: Existing Block Must Be Deleted First

### Problem

`GenerateBlocksFromSource()` in V20 does **not** auto-overwrite an existing block. If a block with the same name already exists in the PLC (in any group), TIA throws:

```
EngineeringTargetInvocationException: The block name 'Modbus' is invalid.
A program element with this fully qualified name already exists in this CPU.
```

This contradicts the V15–V19 behaviour where the ExternalSourceGroup approach would silently overwrite.

### Fix

Before calling `GenerateBlocksFromSource()`, parse the block name from the SCL source using a regex and delete the existing block:

```csharp
var match = Regex.Match(sclCode,
    @"(?:FUNCTION|FUNCTION_BLOCK|DATA_BLOCK|ORGANIZATION_BLOCK)\s+""([^""]+)""",
    RegexOptions.IgnoreCase);
if (match.Success)
{
    var existingBlock = FindBlock(plcSw.BlockGroup, match.Groups[1].Value);
    existingBlock?.Delete();   // removes from whichever group it lives in
}
```

`FindBlock` recurses into all block subgroups, so the block is found regardless of its group location.

### Impact

- The block's group assignment is **not preserved** — after deletion and re-generation, TIA places the new block in the root "Program blocks" group. If you need it in a specific subgroup, move it after import (or set the `blockGroup` parameter for post-import relocation — not yet implemented).
- This applies to all block types: FC, FB, DB, OB.

---

## 2. ImportOptions Namespace Change

### Problem

V20 moved `ImportOptions` from `Siemens.Engineering.SW.Blocks` to the root `Siemens.Engineering` namespace. Code referencing the old location threw `TypeLoadException`.

### Fix

Try both namespaces at startup:

```csharp
_importOptionsType =
    _asm.GetType("Siemens.Engineering.ImportOptions") ??
    _asm.GetType("Siemens.Engineering.SW.Blocks.ImportOptions") ??
    throw new InvalidOperationException("Cannot find ImportOptions type");
```

---

## 3. Generic-Only GetService

### Problem

V20 removed the non-generic `GetService(Type serviceType)` overload on device items. Code like `item.GetService(softwareContainerType)` failed at runtime.

### Fix

Added a `CallGetService()` helper that resolves and calls the generic `GetService<T>()` via reflection:

```csharp
private static object? CallGetService(object target, Type serviceType)
{
    var method = target.GetType()
        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .FirstOrDefault(m => m.Name == "GetService" && m.IsGenericMethodDefinition)
        ?.MakeGenericMethod(serviceType);
    return method?.Invoke(target, null);
}
```

---

## 4. PlcSoftware Discovery (Dual Path)

### Problem

V15–V19 obtained `PlcSoftware` via `GetService<SoftwareContainer>().Software`. V20 also supports a direct `GetService<PlcSoftware>()` path.

### Fix

`TryGetPlcSoftware()` tries both paths in order:

1. `GetService<SoftwareContainer>().Software` (V15–V19 primary path)
2. `GetService<PlcSoftware>()` (V20+ direct path)

Returns `null` for non-PLC device items or if both paths fail.

---

## 5. ICompilable Resolution

### Problem

`Compile()` is explicitly implemented on TIA Portal V20 proxy objects, so dynamic dispatch (`((dynamic)compiler).Compile()`) fails with `'object' does not contain a definition for 'Compile'`.

Additionally, `ICompilable` may live on the PlcSoftware, the CPU DeviceItem, or the Device — depending on version.

### Fix

1. Walk up the hierarchy: PlcSoftware → CPU DeviceItem → Device, calling `GetService<ICompilable>()` at each level.
2. Use `GetInterfaceMap` to resolve the explicitly-implemented `Compile()` method:

```csharp
var ifaceCompile = compilableType.GetMethod("Compile", Type.EmptyTypes);
var map = compiler.GetType().GetInterfaceMap(compilableType);
int idx = Array.IndexOf(map.InterfaceMethods, ifaceCompile);
if (idx >= 0) compileImpl = map.TargetMethods[idx];
```

---

## 6. Registry Discovery (V20 Layout)

### Problem

V20 registers under Openness key `20.0` (not `20`) and stores the DLL path differently:
- **V15–V19:** `InstallationPath` value → `<path>\PublicAPI\<Label>\Siemens.Engineering.dll`
- **V20:** No `InstallationPath`. DLL path stored under `PublicAPI\{assemblyVer}\Siemens.Engineering` value.

### Fix

`TiaVersionResolver.FindInstallations()` now tries both layouts:

```
Layout A (V15–V19): <InstallationPath>\PublicAPI\V18\Siemens.Engineering.dll
Layout B (V20+):    Registry key PublicAPI\{x.0.0.0}\Siemens.Engineering = <full DLL path>
```

Also added `"20.0" → "V20"` mapping to `KnownVersions` and relaxed version matching to handle `StartsWith(normalized + ".")`.

---

## 7. Error Unwrapping

### Problem

Reflection-based calls wrap real exceptions in `TargetInvocationException`, hiding the actual error message from tool output.

### Fix

The `Error()` helper now unwraps the full inner exception chain:

```csharp
var inner = ex;
while (inner.InnerException != null) inner = inner.InnerException;
```

---

## 8. Compiler Message Severity

### Problem

The original code read `msg.PathDescription` for severity. V20 compiler messages expose severity via `msg.State` (an enum: `Error`, `Warning`, `Information`).

### Fix

Changed to `msg.State.ToString()` and error detection to `Equals("Error")` instead of `Contains("Error")`.

---

## SimaticML Reference (V20)

For future reference if SimaticML XML import is ever needed (e.g. for LAD/FBD blocks):

| Property | Value |
|---|---|
| Interface namespace | `http://www.siemens.com/automation/Openness/SW/Interface/v5` |
| LAD body namespace | `FlgNet/v5` |
| SCL body namespace | `StructuredText/v2` |
| Required attributes | `MemoryLayout=Optimized`, `SetENOAutomatically=false` |
| SCL body format | Tokenized AST (`Access`, `Token`, `Parameter`, `Text`, `Comment`, `LineComment`, `Blank`, `NewLine`) — **not** a `<Code>` element |
| VAR sections | Must be XML in `AttributeList/Interface`, not embedded in SCL body |
