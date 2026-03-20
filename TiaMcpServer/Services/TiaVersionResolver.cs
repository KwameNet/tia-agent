// Services/TiaVersionResolver.cs
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TiaMcpServer.Services
{
    /// <summary>
    /// Detects installed TIA Portal versions from the Windows registry and resolves
    /// the correct Siemens.Engineering.dll path at runtime.
    /// </summary>
    public class TiaVersionResolver
    {
        // Registry roots where Siemens registers TIA Portal installations.
        // Both 32-bit and 64-bit registry hives are checked.
        private static readonly string[] RegistryRoots = new[]
        {
            @"SOFTWARE\Siemens\Automation\Openness",
            @"SOFTWARE\WOW6432Node\Siemens\Automation\Openness"
        };

        // Known TIA Portal version numbers mapped to their version labels.
        // Add new entries here when Siemens releases a new version.
        private static readonly Dictionary<string, string> KnownVersions = new()
        {
            { "15.1", "V15.1" },
            { "16",   "V16"   },
            { "17",   "V17"   },
            { "18",   "V18"   },
            { "19",   "V19"   },
            { "20",   "V20"   },
            { "20.0", "V20"   },  // V20 registers as "20.0" in the registry
        };

        public record TiaInstallation(string Version, string Label, string DllPath);

        /// <summary>
        /// Returns all detected TIA Portal installations on this machine,
        /// ordered from highest version to lowest.
        /// </summary>
        public static List<TiaInstallation> DetectInstallations()
        {
            var found = new List<TiaInstallation>();

            foreach (var root in RegistryRoots)
            {
                using var baseKey = Registry.LocalMachine.OpenSubKey(root);
                if (baseKey == null) continue;

                foreach (var versionKey in baseKey.GetSubKeyNames())
                {
                    using var subKey = baseKey.OpenSubKey(versionKey);
                    if (subKey == null) continue;

                    if (!KnownVersions.TryGetValue(versionKey, out var label))
                        label = $"V{versionKey}"; // fallback for future versions

                    // ── Layout A (V15.1–V19): InstallationPath value present ───────────
                    // Pattern: <InstallationPath>\PublicAPI\<Label>\Siemens.Engineering.dll
                    var installPath = subKey.GetValue("InstallationPath") as string;
                    if (!string.IsNullOrWhiteSpace(installPath))
                    {
                        var dllPath = Path.Combine(
                            installPath, "PublicAPI", label, "Siemens.Engineering.dll");

                        if (!File.Exists(dllPath))
                        {
                            // Fallback: some versions place the DLL directly under PublicAPI
                            dllPath = Path.Combine(installPath, "PublicAPI", "Siemens.Engineering.dll");
                        }

                        if (File.Exists(dllPath))
                        {
                            found.Add(new TiaInstallation(versionKey, label, dllPath));
                            continue;
                        }
                    }

                    // ── Layout B (V20+): DLL path stored in PublicAPI\{assemblyVer}\Siemens.Engineering ──
                    // Pattern: HKLM\...\Openness\{ver}\PublicAPI\{x.0.0.0}\Siemens.Engineering = <path>
                    using var publicApiKey = subKey.OpenSubKey("PublicAPI");
                    if (publicApiKey == null) continue;

                    string? resolvedDll = null;
                    foreach (var asmVer in publicApiKey.GetSubKeyNames().OrderByDescending(s => s))
                    {
                        using var asmKey = publicApiKey.OpenSubKey(asmVer);
                        if (asmKey == null) continue;
                        var candidate = asmKey.GetValue("Siemens.Engineering") as string;
                        if (!string.IsNullOrWhiteSpace(candidate) && File.Exists(candidate))
                        {
                            resolvedDll = candidate;
                            break;
                        }
                    }

                    if (resolvedDll != null)
                        found.Add(new TiaInstallation(versionKey, label, resolvedDll));
                }
            }

            // Deduplicate (both registry hives may return the same entry) and sort descending.
            return found
                .GroupBy(i => i.DllPath)
                .Select(g => g.First())
                .OrderByDescending(i => i.Version, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        /// <summary>
        /// Resolves the best available installation.
        /// If <paramref name="preferredVersion"/> is specified (e.g. "V18"), that version
        /// is selected. Otherwise the highest installed version is used.
        /// Throws <see cref="InvalidOperationException"/> if no installation is found.
        /// </summary>
        public static TiaInstallation Resolve(string? preferredVersion = null)
        {
            var installations = DetectInstallations();

            if (installations.Count == 0)
                throw new InvalidOperationException(
                    "No TIA Portal installation found. " +
                    "Ensure TIA Portal is installed and Openness is enabled in its settings.");

            if (!string.IsNullOrWhiteSpace(preferredVersion))
            {
                // Normalize: accept "V18", "v18", "18", "V20", "20"
                var normalized = preferredVersion.TrimStart('v', 'V');
                var match = installations.FirstOrDefault(
                    i => i.Version == normalized ||
                         i.Version.StartsWith(normalized + ".") ||
                         i.Label.Equals($"V{normalized}", StringComparison.OrdinalIgnoreCase));

                if (match == null)
                {
                    var available = string.Join(", ", installations.Select(i => i.Label));
                    throw new InvalidOperationException(
                        $"TIA Portal {preferredVersion} not found. " +
                        $"Installed versions: {available}");
                }
                return match;
            }

            // No preference — use the highest available version.
            return installations[0];
        }
    }
}
