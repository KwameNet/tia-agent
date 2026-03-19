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

                    // The registry value "InstallationPath" points to the TIA Portal root.
                    var installPath = subKey.GetValue("InstallationPath") as string;
                    if (string.IsNullOrWhiteSpace(installPath)) continue;

                    // Resolve the PublicAPI DLL path from the installation root.
                    // Pattern: <InstallationPath>\PublicAPI\<Label>\Siemens.Engineering.dll
                    if (!KnownVersions.TryGetValue(versionKey, out var label))
                        label = $"V{versionKey}"; // fallback for future versions

                    var dllPath = Path.Combine(
                        installPath, "PublicAPI", label, "Siemens.Engineering.dll");

                    if (!File.Exists(dllPath))
                    {
                        // Fallback: some installations place the DLL directly under PublicAPI
                        dllPath = Path.Combine(installPath, "PublicAPI", "Siemens.Engineering.dll");
                        if (!File.Exists(dllPath)) continue;
                    }

                    found.Add(new TiaInstallation(versionKey, label, dllPath));
                }
            }

            // Deduplicate (both registry hives may return the same entry) and sort descending.
            return found
                .DistinctBy(i => i.DllPath)
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
                // Normalize: accept "V18", "v18", "18"
                var normalized = preferredVersion.TrimStart('v', 'V');
                var match = installations.FirstOrDefault(
                    i => i.Version == normalized || i.Label.Equals(
                        $"V{normalized}", StringComparison.OrdinalIgnoreCase));

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
