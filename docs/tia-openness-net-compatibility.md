# TIA Portal Openness API — .NET Compatibility Fix

## Problem

When calling `TiaAttach` (or any tool that invokes the Siemens Openness API at runtime), the server threw:

```
MissingMethodException: Method not found:
'System.Reflection.Assembly System.Reflection.Assembly.Load(Byte[], Byte[], System.Security.SecurityContextSource)'
```

### Root Cause

The Siemens `Siemens.Engineering.dll` (TIA Portal Openness API) internally calls the overload:

```csharp
Assembly.Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
```

This overload exists in **.NET Framework** but was **removed in .NET Core / .NET 5+**. The project was originally targeting `net9.0-windows`, causing a `MissingMethodException` at runtime whenever the Openness API tried to load or attach to a TIA Portal process.

## Fix

Changed the target framework in `TiaMcpServer/TiaMcpServer.csproj` from `net9.0-windows` to `net48`:

```xml
<!-- Before -->
<TargetFramework>net9.0-windows</TargetFramework>

<!-- After -->
<TargetFramework>net48</TargetFramework>
```

Also removed the explicit `<Reference Include="Microsoft.CSharp" />` item group while cleaning stale publish output (it picks up the GAC version automatically on net48 and must not reference a leftover .NET 9 DLL from a previous publish), then re-added it so the `dynamic` keyword compiles correctly:

```xml
<ItemGroup>
  <Reference Include="Microsoft.CSharp" />
</ItemGroup>
```

The `Polyfills.cs` file (which adds `System.Runtime.CompilerServices.IsExternalInit`) was already present for this reason — it provides C# 9+ init-only setter support on .NET Framework 4.8.

## Rebuild Steps

```bash
cd TiaMcpServer
dotnet clean -c Release
rm -rf publish obj        # remove stale .NET 9 binaries
dotnet publish -c Release -o publish
```

Then **restart Claude Code** so the MCP server process loads the new `publish/TiaMcpServer.exe`.

## Key Rule

The Siemens TIA Portal Openness API **must run on .NET Framework 4.8** (or earlier). It cannot run on .NET Core, .NET 5, or later because it relies on removed `System.Reflection.Assembly` overloads. Any host process (MCP server, console app, etc.) that loads `Siemens.Engineering.dll` must target `net48` or equivalent.
