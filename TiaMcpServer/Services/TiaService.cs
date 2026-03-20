// Services/TiaService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TiaMcpServer.Services
{
    public class TiaService : IDisposable
    {
        // ── Resolved installation info ───────────────────────────────────────
        public string InstalledVersion { get; private init; }
        public string DllPath          { get; private init; }

        // ── Live TIA Portal objects (typed as dynamic — loaded at runtime) ───
        private dynamic? _portal;
        private dynamic? _project;

        // Expose to TiaReadService via internal access within the same assembly
        internal dynamic? Project => _project;
        internal Assembly Asm     => _asm;

        // ── Openness assembly + key types ────────────────────────────────────
        private readonly Assembly   _asm;
        private readonly Type       _tiaPortalType;
        private readonly object     _withoutUiMode;   // TiaPortalMode.WithoutUserInterface
        private readonly Type       _importOptionsType;
        private readonly object     _importOverride;  // ImportOptions.Override

        public TiaService()
        {
            // 1. Detect version — honour TIA_VERSION env var if set, else use highest.
            var preferredVersion = Environment.GetEnvironmentVariable("TIA_VERSION");
            var installation = TiaVersionResolver.Resolve(preferredVersion);

            InstalledVersion = installation.Label;
            DllPath          = installation.DllPath;

            // 2. Load Siemens.Engineering.dll dynamically.
            _asm = Assembly.LoadFrom(DllPath);

            // 3. Cache frequently used types and enum values.
            _tiaPortalType   = _asm.GetType("Siemens.Engineering.TiaPortal")!;
            var modeType     = _asm.GetType("Siemens.Engineering.TiaPortalMode")!;
            _withoutUiMode   = Enum.Parse(modeType, "WithoutUserInterface");

            // V20+ moved ImportOptions to the root namespace; older versions used SW.Blocks.
            _importOptionsType =
                _asm.GetType("Siemens.Engineering.ImportOptions") ??
                _asm.GetType("Siemens.Engineering.SW.Blocks.ImportOptions") ??
                throw new InvalidOperationException(
                    "Cannot find ImportOptions type in Siemens.Engineering.dll. " +
                    $"TIA Portal version: {InstalledVersion}");
            _importOverride = Enum.Parse(_importOptionsType, "Override");
        }

        // ── Project ──────────────────────────────────────────────────────────

        /// <summary>
        /// Attaches to an already-running TIA Portal instance and grabs the first open project.
        /// Use this when TIA Portal is already open with a project — no need to call OpenProject.
        /// </summary>
        public object AttachToRunning()
        {
            try
            {
                // TiaPortal.GetProcesses() — static method returning IList<TiaPortalProcess>
                var getProcesses = _tiaPortalType.GetMethod(
                    "GetProcesses",
                    BindingFlags.Public | BindingFlags.Static,
                    null, Type.EmptyTypes, null);

                if (getProcesses == null)
                    return new { success = false, error = "GetProcesses not found — check Openness API version." };

                dynamic processes = getProcesses.Invoke(null, null)!;

                int count = 0;
                foreach (var _ in processes) count++;
                if (count == 0)
                    return new { success = false, error = "No running TIA Portal process found. Start TIA Portal and open a project first." };

                // Attach to the first running process
                dynamic process = null!;
                foreach (dynamic p in processes) { process = p; break; }

                _portal?.Dispose();
                _portal = process.Attach();

                // Get the first open project
                int projectCount = 0;
                foreach (var _ in _portal!.Projects) projectCount++;
                if (projectCount == 0)
                    return new { success = false, error = "TIA Portal is running but no project is open. Open a project in TIA Portal first." };

                foreach (dynamic proj in _portal!.Projects) { _project = proj; break; }

                return new { success = true, message = $"Attached to: {_project!.Name}", version = InstalledVersion };
            }
            catch (Exception ex) { return Error(ex); }
        }

        public object OpenProject(string projectPath)
        {
            try
            {
                _portal?.Dispose();
                _portal  = Activator.CreateInstance(_tiaPortalType, _withoutUiMode);
                _project = _portal!.Projects.Open(new FileInfo(projectPath));
                return new { success = true, message = $"Opened: {_project!.Name}", version = InstalledVersion };
            }
            catch (Exception ex) { return Error(ex); }
        }

        public object SaveProject()
        {
            try { AssertProject(); _project!.Save(); return Ok("Project saved."); }
            catch (Exception ex) { return Error(ex); }
        }

        public object CloseProject(bool save)
        {
            try
            {
                AssertProject();
                if (save) _project!.Save();
                _project!.Close();
                _project = null;
                return Ok("Project closed.");
            }
            catch (Exception ex) { return Error(ex); }
        }

        // ── PLCs ─────────────────────────────────────────────────────────────

        public object ListPlcs()
        {
            try
            {
                AssertProject();
                var plcs = GetAllPlcSoftware()
                    .Select(p => new { name = GetPlcName(p) })
                    .ToList();
                return new { success = true, plcs };
            }
            catch (Exception ex) { return Error(ex); }
        }

        // ── Blocks ───────────────────────────────────────────────────────────

        public object ListBlocks(string plcName)
        {
            try
            {
                AssertProject();
                var blocks = new List<object>();
                foreach (var plcSw in GetTargetPlcs(plcName))
                    CollectBlocks(plcSw.BlockGroup, blocks, GetPlcName(plcSw));
                return new { success = true, blocks };
            }
            catch (Exception ex) { return Error(ex); }
        }

        public object ImportSclBlock(string plcName, string blockGroup, string sclCode)
        {
            try
            {
                AssertProject();
                Console.Error.WriteLine("[TIA-v3] ImportSclBlock called — ExternalSourceGroup path");
                var tempFile = Path.Combine(Path.GetTempPath(), $"tia_{Guid.NewGuid():N}.scl");
                File.WriteAllText(tempFile, sclCode, System.Text.Encoding.UTF8);
                try
                {
                    foreach (var plcSw in GetTargetPlcs(plcName))
                    {
                        // Get the ExternalSourceGroup via reflection
                        dynamic extGroup = ((dynamic)plcSw).ExternalSourceGroup;
                        var extGroupType = ((object)extGroup).GetType();
                        Console.Error.WriteLine($"[TIA] ExternalSourceGroup type: {extGroupType.FullName}");

                        // Discover the external-sources collection.
                        // Enumerate all properties first for diagnostics, then try to access the collection.
                        var allProps = extGroupType.GetProperties(
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        var propNames = string.Join(", ", allProps.Select(p => $"{p.Name}({p.PropertyType.Name})"));
                        Console.Error.WriteLine($"[TIA-diag] All properties on {extGroupType.FullName}: [{propNames}]");

                        // Also check interfaces
                        var ifaces = extGroupType.GetInterfaces();
                        Console.Error.WriteLine($"[TIA-diag] Interfaces: [{string.Join(", ", ifaces.Select(i => i.Name))}]");

                        dynamic extSrcFiles;
                        // Try known property names via reflection first (avoids RuntimeBinderException)
                        var extSrcProp = allProps.FirstOrDefault(p =>
                            p.Name == "ExternalSources" || p.Name == "ExternalSourceFiles");
                        if (extSrcProp == null)
                        {
                            // Broader search
                            extSrcProp = allProps.FirstOrDefault(p =>
                                p.Name.Contains("ExternalSource") || p.Name.Contains("Sources"));
                        }
                        if (extSrcProp != null)
                        {
                            Console.Error.WriteLine($"[TIA] Using property via reflection: {extSrcProp.Name}");
                            extSrcFiles = extSrcProp.GetValue(extGroup)!;
                        }
                        else
                        {
                            // Last resort: try dynamic dispatch
                            try { extSrcFiles = extGroup.ExternalSources; }
                            catch
                            {
                                throw new InvalidOperationException(
                                    $"[DIAG] Cannot find external sources on {extGroupType.FullName}. " +
                                    $"Properties: [{propNames}]. Interfaces: [{string.Join(", ", ifaces.Select(i => i.FullName))}]");
                            }
                        }

                        // Remove any leftover external source with the same file name
                        string srcName = Path.GetFileName(tempFile);
                        dynamic? existing = null;
                        foreach (dynamic s in extSrcFiles)
                            if (((string)s.Name).Equals(srcName, StringComparison.OrdinalIgnoreCase))
                            { existing = s; break; }
                        existing?.Delete();

                        // Delete the existing block so GenerateBlocksFromSource can create it fresh.
                        // V20 does not auto-overwrite; it throws if the block name already exists.
                        var blockNameMatch = System.Text.RegularExpressions.Regex.Match(
                            sclCode, @"(?:FUNCTION|FUNCTION_BLOCK|DATA_BLOCK|ORGANIZATION_BLOCK)\s+""([^""]+)""",
                            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        if (blockNameMatch.Success)
                        {
                            string parsedName = blockNameMatch.Groups[1].Value;
                            Console.Error.WriteLine($"[TIA] Parsed block name from SCL: '{parsedName}'");

                            var existingBlock = FindBlock(plcSw.BlockGroup, parsedName);
                            Console.Error.WriteLine($"[TIA] FindBlock result: {(existingBlock != null ? "FOUND" : "NOT FOUND")}");

                            if (existingBlock != null)
                            {
                                // Try dynamic dispatch first, then reflection
                                try
                                {
                                    existingBlock.Delete();
                                    Console.Error.WriteLine("[TIA] Block.Delete() via dynamic dispatch succeeded");
                                }
                                catch (Exception delEx)
                                {
                                    Console.Error.WriteLine($"[TIA] Block.Delete() via dynamic failed: {delEx.Message}");
                                    var deleteMethod = ((object)existingBlock).GetType().GetMethod("Delete", Type.EmptyTypes);
                                    if (deleteMethod != null)
                                    {
                                        deleteMethod.Invoke(existingBlock, null);
                                        Console.Error.WriteLine("[TIA] Block.Delete() via reflection succeeded");
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException(
                                            $"Cannot delete existing block '{parsedName}': Delete() method not found. " +
                                            $"Block type: {((object)existingBlock).GetType().FullName}");
                                    }
                                }

                                // Verify block is actually gone — fail hard if not
                                var check = FindBlock(plcSw.BlockGroup, parsedName);
                                if (check != null)
                                {
                                    Console.Error.WriteLine("[TIA] WARNING: Block still exists after Delete(). Trying reflection on re-found block...");
                                    var deleteMethod = ((object)check).GetType().GetMethod("Delete", Type.EmptyTypes);
                                    deleteMethod?.Invoke(check, null);

                                    var check2 = FindBlock(plcSw.BlockGroup, parsedName);
                                    if (check2 != null)
                                        throw new InvalidOperationException(
                                            $"Block '{parsedName}' could not be deleted — it still exists after multiple attempts. " +
                                            $"Delete it manually in TIA Portal before importing.");
                                }
                                Console.Error.WriteLine("[TIA] Verified: block is deleted");
                            }
                        }
                        else
                        {
                            Console.Error.WriteLine($"[TIA] WARNING: Could not parse block name from SCL");
                        }

                        // Import the SCL file as an external source and generate the block.
                        Console.Error.WriteLine("[TIA] Calling CreateFromFile...");
                        var extSrcFilesType = ((object)extSrcFiles).GetType();
                        var createMethods = extSrcFilesType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                            .Where(m => m.Name == "CreateFromFile").ToArray();
                        Console.Error.WriteLine($"[TIA] CreateFromFile overloads: {createMethods.Length}");
                        foreach (var m in createMethods)
                        {
                            var parms = m.GetParameters();
                            Console.Error.WriteLine($"[TIA]   ({string.Join(", ", parms.Select(p => $"{p.ParameterType.Name} {p.Name}"))})");
                        }

                        dynamic extSrc;
                        // Try (string, string) first, then (FileInfo)
                        var twoStringOverload = createMethods.FirstOrDefault(m =>
                            m.GetParameters().Length == 2 &&
                            m.GetParameters().All(p => p.ParameterType == typeof(string)));
                        if (twoStringOverload != null)
                        {
                            Console.Error.WriteLine("[TIA] Using CreateFromFile(string, string)");
                            extSrc = twoStringOverload.Invoke(extSrcFiles, new object[] { srcName, tempFile })!;
                        }
                        else
                        {
                            var fileInfoOverload = createMethods.FirstOrDefault(m =>
                                m.GetParameters().Length == 1 &&
                                m.GetParameters()[0].ParameterType == typeof(FileInfo));
                            if (fileInfoOverload != null)
                            {
                                Console.Error.WriteLine("[TIA] Using CreateFromFile(FileInfo)");
                                extSrc = fileInfoOverload.Invoke(extSrcFiles, new object[] { new FileInfo(tempFile) })!;
                            }
                            else
                            {
                                // Last resort: try dynamic dispatch
                                Console.Error.WriteLine("[TIA] No matching CreateFromFile overload found, trying dynamic");
                                try { extSrc = extSrcFiles.CreateFromFile(srcName, tempFile); }
                                catch { extSrc = extSrcFiles.CreateFromFile(new FileInfo(tempFile)); }
                            }
                        }
                        Console.Error.WriteLine("[TIA] CreateFromFile succeeded. Calling GenerateBlocksFromSource...");
                        try   { extSrc.GenerateBlocksFromSource(); }
                        finally { try { extSrc.Delete(); } catch { /* best-effort cleanup */ } }
                        Console.Error.WriteLine("[TIA] GenerateBlocksFromSource succeeded");
                    }
                }
                finally { File.Delete(tempFile); }
                return Ok("Block imported successfully.");
            }
            catch (Exception ex) { return Error(ex); }
        }

        public object GetBlockSource(string plcName, string blockName, string blockType)
        {
            try
            {
                AssertProject();
                foreach (var plcSw in GetTargetPlcs(plcName))
                {
                    var block = FindBlock(plcSw.BlockGroup, blockName);
                    if (block == null) continue;
                    var tempFile = Path.Combine(Path.GetTempPath(), $"{blockName}_{Guid.NewGuid():N}.scl");
                    block.Export(new FileInfo(tempFile), 0 /* ExportOptions.None */);
                    var source = File.ReadAllText(tempFile);
                    File.Delete(tempFile);
                    return new { success = true, plc = plcName, block = blockName, source };
                }
                return new { success = false, message = $"Block '{blockName}' not found." };
            }
            catch (Exception ex) { return Error(ex); }
        }

        // ── Compile ──────────────────────────────────────────────────────────

        public object Compile(string plcName)
        {
            try
            {
                AssertProject();
                var messages = new List<object>();
                var compilableType = _asm.GetType("Siemens.Engineering.Compiler.ICompilable")
                    ?? throw new InvalidOperationException("ICompilable not found in Siemens.Engineering.dll");

                foreach (var plcSw in GetTargetPlcs(plcName))
                {
                    // Try ICompilable on the PlcSoftware itself first (V20+),
                    // then the CPU DeviceItem (plcSw.Parent), then the Device (plcSw.Parent.Parent).
                    object? compiler = CallGetService((object)plcSw, compilableType);
                    if (compiler == null)
                    {
                        object? cpuItem = (object?)((dynamic)plcSw).Parent;
                        compiler = cpuItem == null ? null : CallGetService(cpuItem, compilableType);
                    }
                    if (compiler == null)
                    {
                        object? deviceItem = (object?)((dynamic)plcSw).Parent?.Parent;
                        compiler = deviceItem == null ? null : CallGetService(deviceItem, compilableType);
                    }
                    if (compiler == null) continue;

                    // ICompilable.Compile() is explicitly implemented on the TIA Portal proxy,
                    // so dynamic dispatch (((dynamic)compiler).Compile()) fails with
                    // "'object' does not contain a definition for 'Compile'".
                    // Use GetInterfaceMap to find the actual explicit implementation and invoke it.
                    MethodInfo? compileImpl = null;
                    try
                    {
                        var ifaceCompile = compilableType.GetMethod("Compile", Type.EmptyTypes);
                        var map = compiler.GetType().GetInterfaceMap(compilableType);
                        int idx = ifaceCompile == null ? -1 : Array.IndexOf(map.InterfaceMethods, ifaceCompile);
                        if (idx >= 0) compileImpl = map.TargetMethods[idx];
                    }
                    catch { }
                    // Fall back to the interface MethodInfo if GetInterfaceMap is unavailable.
                    compileImpl ??= compilableType.GetMethod("Compile", Type.EmptyTypes);
                    if (compileImpl == null) continue;
                    dynamic result = compileImpl.Invoke(compiler, null);
                    CollectCompilerMessages(result.Messages, GetPlcName(plcSw), messages);
                }

                bool hasErrors = messages.Any(m =>
                    ((string)((dynamic)m).severity)
                        .Equals("Error", StringComparison.OrdinalIgnoreCase));

                return new { success = !hasErrors, errors = hasErrors, messages };
            }
            catch (Exception ex) { return Error(ex); }
        }

        // ── Hardware ─────────────────────────────────────────────────────────

        public object ConfigureHardware(string deviceName, string mlfb, string rackSlot)
        {
            try
            {
                AssertProject();
                _project!.Devices.CreateWithItem(
                    $"OrderNumber:{mlfb}", deviceName, deviceName);
                return Ok($"Device '{deviceName}' added (MLFB: {mlfb}).");
            }
            catch (Exception ex) { return Error(ex); }
        }

        // ── Attach to running instance ───────────────────────────────────────

        /// <summary>
        /// Lists all running TIA Portal processes that can be attached to.
        /// </summary>
        public object ListRunningInstances()
        {
            try
            {
                var processes = GetRunningProcesses();
                var list = processes.Select((p, i) => new
                {
                    index       = i,
                    id          = (int)p.Id,
                    mode        = p.Mode.ToString(),
                    projectPath = p.ProjectPath?.FullName ?? "(no project)"
                }).ToList();
                return new { success = true, count = list.Count, instances = list };
            }
            catch (Exception ex) { return Error(ex); }
        }

        /// <summary>
        /// Attaches to an already-running TIA Portal instance.
        /// If processIndex is omitted and exactly one instance is running it is used automatically.
        /// After attaching, the first open project (if any) becomes the active project.
        /// </summary>
        public object AttachToRunningInstance(int? processIndex = null)
        {
            try
            {
                var processes = GetRunningProcesses().ToList();

                if (processes.Count == 0)
                    return new { success = false, message = "No running TIA Portal instances found." };

                if (processIndex == null && processes.Count > 1)
                    return new { success = false, message = $"{processes.Count} instances are running. Specify processIndex (0-{processes.Count - 1})." };

                var target = processes[processIndex ?? 0];

                // Dispose any existing portal before attaching.
                _portal?.Dispose();
                _portal  = null;
                _project = null;

                _portal = target.Attach();

                // Grab the first open project, if any.
                dynamic? proj = null;
                foreach (dynamic p in _portal!.Projects)
                { proj = p; break; }
                _project = proj;

                var projectName = _project != null ? (string)_project.Name : "(none)";
                return new
                {
                    success     = true,
                    message     = $"Attached to TIA Portal process {(int)target.Id}.",
                    project     = projectName,
                    processId   = (int)target.Id,
                    mode        = target.Mode.ToString()
                };
            }
            catch (Exception ex) { return Error(ex); }
        }

        private IEnumerable<dynamic> GetRunningProcesses()
        {
            // TiaPortal.GetProcesses() is a static method returning IList<TiaPortalProcess>
            var method = _tiaPortalType.GetMethod(
                "GetProcesses",
                BindingFlags.Static | BindingFlags.Public,
                null, Type.EmptyTypes, null)
                ?? throw new InvalidOperationException(
                    "TiaPortal.GetProcesses() not found — check your Openness API version.");

            var result = method.Invoke(null, null)
                ?? throw new InvalidOperationException("TiaPortal.GetProcesses() returned null.");

            return ((System.Collections.IEnumerable)result).Cast<dynamic>();
        }

        // ── Version info tool ────────────────────────────────────────────────

        /// <summary>
        /// Returns all detected TIA Portal installations on this machine.
        /// Exposed as a tool so Claude can report available versions to the user.
        /// </summary>
        public object GetInstalledVersions()
        {
            try
            {
                var installations = TiaVersionResolver.DetectInstallations()
                    .Select(i => new { version = i.Label, dllPath = i.DllPath })
                    .ToList();
                return new { success = true, active = InstalledVersion, installations };
            }
            catch (Exception ex) { return Error(ex); }
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private void AssertProject()
        {
            if (_project == null)
                throw new InvalidOperationException(
                    "No project is open. Call TiaOpenProject first.");
        }

        private IEnumerable<dynamic> GetAllPlcSoftware()
        {
            var softwareContainerType = _asm.GetType("Siemens.Engineering.HW.Features.SoftwareContainer");
            var plcSoftwareType       = _asm.GetType("Siemens.Engineering.SW.PlcSoftware");

            foreach (dynamic device in _project!.Devices)
            {
                // Collect items first so we can wrap the enumeration in try/catch
                // without putting yield return inside a try-catch block.
                var items = new List<object>();
                try { foreach (dynamic item in (dynamic)device.DeviceItems) items.Add((object)item); }
                catch { continue; }

                foreach (var item in items)
                {
                    dynamic? sw = TryGetPlcSoftware(item, softwareContainerType, plcSoftwareType);
                    if (sw != null) yield return sw;
                }
            }
        }

        /// <summary>
        /// Tries to obtain the PlcSoftware from a device item using two paths:
        ///   1. GetService&lt;SoftwareContainer&gt;().Software  (V15–V19 primary path)
        ///   2. GetService&lt;PlcSoftware&gt;()               (V20+ direct path)
        /// Returns null for non-PLC items or if both paths fail.
        /// </summary>
        private dynamic? TryGetPlcSoftware(object item, Type? containerType, Type? plcSwType)
        {
            if (containerType != null)
            {
                try
                {
                    var container = CallGetService(item, containerType);
                    if (container != null)
                    {
                        var sw = container.GetType().GetProperty("Software")?.GetValue(container);
                        if (sw?.GetType().Name == "PlcSoftware") return (dynamic)sw;
                    }
                }
                catch { /* item does not support SoftwareContainer */ }
            }

            if (plcSwType != null)
            {
                try
                {
                    var sw = CallGetService(item, plcSwType);
                    if (sw?.GetType().Name == "PlcSoftware") return (dynamic)sw;
                }
                catch { /* item does not support PlcSoftware */ }
            }

            return null;
        }

        /// <summary>
        /// Calls the generic GetService&lt;T&gt;() method via reflection.
        /// TIA Portal V20 removed the non-generic GetService(Type) overload.
        /// </summary>
        private static object? CallGetService(object target, Type serviceType)
        {
            var method = target.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name == "GetService" && m.IsGenericMethodDefinition)
                ?.MakeGenericMethod(serviceType);
            return method?.Invoke(target, null);
        }

        private IEnumerable<dynamic> GetTargetPlcs(string plcName)
        {
            var all = GetAllPlcSoftware();
            return string.IsNullOrWhiteSpace(plcName)
                ? all
                : all.Where(p => ((string)GetPlcName(p))
                    .Equals(plcName, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetPlcName(dynamic plcSw)
            => (string)(plcSw.Parent?.Parent?.Name ?? "Unknown");

        private void CollectBlocks(dynamic group, List<object> result, string plcName)
        {
            foreach (dynamic block in group.Blocks)
                result.Add(new {
                    plc      = plcName,
                    name     = (string)block.Name,
                    type     = block.GetType().Name,
                    number   = (int)block.Number,
                    language = block.ProgrammingLanguage.ToString(),
                    group    = (string)group.Name
                });
            foreach (dynamic sub in group.Groups)
                CollectBlocks(sub, result, plcName);
        }

        private dynamic? FindBlock(dynamic group, string name)
        {
            foreach (dynamic b in group.Blocks)
                if (((string)b.Name).Equals(name, StringComparison.OrdinalIgnoreCase))
                    return b;
            foreach (dynamic sub in group.Groups)
            {
                var found = FindBlock(sub, name);
                if (found != null) return found;
            }
            return null;
        }

        private static dynamic ResolveOrCreateGroup(dynamic root, string path)
        {
            var parts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            dynamic current = root;
            foreach (var part in parts.Skip(1))
            {
                dynamic? sub = null;
                foreach (dynamic g in current.Groups)
                    if (((string)g.Name).Equals(part, StringComparison.OrdinalIgnoreCase))
                    { sub = g; break; }
                current = sub ?? current.Groups.Create(part);
            }
            return current;
        }

        /// Recursively collect compiler messages — the Openness API nests
        /// block-level errors inside PLC-level summary nodes.
        private static void CollectCompilerMessages(
            dynamic messages, string plcName, List<object> results)
        {
            foreach (dynamic msg in messages)
            {
                string desc = (string)msg.Description;
                string state = msg.State.ToString();
                string path = (string)msg.Path;

                // Only add leaf messages that have actual content
                if (!string.IsNullOrWhiteSpace(desc))
                {
                    results.Add(new
                    {
                        plc      = plcName,
                        severity = state,
                        text     = desc,
                        path     = path
                    });
                }

                // Recurse into child messages
                CollectCompilerMessages(msg.Messages, plcName, results);
            }
        }

        private static object Ok(string message)    => new { success = true,  message };
        private static object Error(Exception ex)
        {
            // Unwrap TargetInvocationException to expose the real cause
            var inner = ex;
            while (inner.InnerException != null) inner = inner.InnerException;
            var msg = "[build-v3] " + (inner == ex
                ? ex.Message
                : $"{ex.Message} → Inner: {inner.GetType().Name}: {inner.Message}");
            return new { success = false, error = msg };
        }

        public void Dispose()
        {
            try { _project?.Close(); } catch { /* ignore */ }
            try { _portal?.Dispose(); } catch { /* ignore */ }
        }
    }
}
