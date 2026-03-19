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

            _importOptionsType = _asm.GetType("Siemens.Engineering.SW.Blocks.ImportOptions")!;
            _importOverride    = Enum.Parse(_importOptionsType, "Override");
        }

        // ── Project ──────────────────────────────────────────────────────────

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
                var tempFile = Path.Combine(Path.GetTempPath(), $"tia_{Guid.NewGuid():N}.scl");
                File.WriteAllText(tempFile, sclCode);
                try
                {
                    foreach (var plcSw in GetTargetPlcs(plcName))
                    {
                        var group = ResolveOrCreateGroup(plcSw.BlockGroup, blockGroup);
                        group.Blocks.Import(new FileInfo(tempFile), _importOverride);
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
                foreach (var plcSw in GetTargetPlcs(plcName))
                {
                    // Access the compiler service via dynamic dispatch
                    dynamic? compiler = ((dynamic)plcSw).Parent?.Parent?
                        .GetService(_asm.GetType("Siemens.Engineering.Compiler.ICompilable")!);
                    if (compiler == null) continue;

                    dynamic result = compiler.Compile();
                    foreach (dynamic msg in result.Messages)
                    {
                        messages.Add(new
                        {
                            plc      = GetPlcName(plcSw),
                            severity = (string)msg.PathDescription,
                            text     = (string)msg.Description,
                            path     = (string)msg.Path
                        });
                    }
                }

                bool hasErrors = messages.Any(m =>
                    ((string)((dynamic)m).severity)
                        .Contains("Error", StringComparison.OrdinalIgnoreCase));

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
            var softwareContainerType =
                _asm.GetType("Siemens.Engineering.HW.Features.SoftwareContainer")!;

            foreach (dynamic device in _project!.Devices)
                foreach (dynamic item in device.DeviceItems)
                {
                    dynamic? sw = item.GetService(softwareContainerType)?.Software;
                    if (sw != null && sw.GetType().Name == "PlcSoftware")
                        yield return sw;
                }
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
            var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
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

        private static object Ok(string message)    => new { success = true,  message };
        private static object Error(Exception ex)   => new { success = false, error = ex.Message };

        public void Dispose()
        {
            try { _project?.Close(); } catch { /* ignore */ }
            try { _portal?.Dispose(); } catch { /* ignore */ }
        }
    }
}
