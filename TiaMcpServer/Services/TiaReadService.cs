// Services/TiaReadService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TiaMcpServer.Services
{
    public class TiaReadService
    {
        private readonly TiaService _tia;

        public TiaReadService(TiaService tia)
        {
            _tia = tia;
        }

        // ── Tag Tables ───────────────────────────────────────────────────────

        /// <summary>
        /// Lists all tag tables defined in a PLC's tag editor.
        /// </summary>
        public object ListTagTables(string plcName)
        {
            try
            {
                AssertProject();
                var tables = new List<object>();

                foreach (var plcSw in GetTargetPlcs(plcName))
                {
                    // PlcSoftware.TagTableGroup.TagTables
                    dynamic tagTableGroup = plcSw.TagTableGroup;
                    foreach (dynamic table in tagTableGroup.TagTables)
                    {
                        tables.Add(new
                        {
                            plc   = GetPlcName(plcSw),
                            name  = (string)table.Name,
                            count = (int)table.Tags.Count
                        });
                    }

                    // Also walk sub-groups recursively
                    CollectTagTablesFromGroups(tagTableGroup.Groups, tables, GetPlcName(plcSw));
                }

                return new { success = true, tables };
            }
            catch (Exception ex) { return Error(ex); }
        }

        private void CollectTagTablesFromGroups(dynamic groups, List<object> result, string plcName)
        {
            foreach (dynamic group in groups)
            {
                foreach (dynamic table in group.TagTables)
                    result.Add(new
                    {
                        plc   = plcName,
                        name  = (string)table.Name,
                        group = (string)group.Name,
                        count = (int)table.Tags.Count
                    });
                CollectTagTablesFromGroups(group.Groups, result, plcName);
            }
        }

        // ── Tags ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Returns all tags in a named tag table.
        /// If tableName is empty, returns tags from all tables in the PLC.
        /// </summary>
        public object GetTags(string plcName, string tableName)
        {
            try
            {
                AssertProject();
                var tags = new List<object>();

                foreach (var plcSw in GetTargetPlcs(plcName))
                {
                    dynamic tagTableGroup = plcSw.TagTableGroup;
                    CollectTagsFromGroup(tagTableGroup, tableName, tags, GetPlcName(plcSw));
                }

                return new { success = true, count = tags.Count, tags };
            }
            catch (Exception ex) { return Error(ex); }
        }

        private void CollectTagsFromGroup(dynamic group, string tableName, List<object> result, string plcName)
        {
            foreach (dynamic table in group.TagTables)
            {
                if (!string.IsNullOrWhiteSpace(tableName) &&
                    !((string)table.Name).Equals(tableName, StringComparison.OrdinalIgnoreCase))
                    continue;

                foreach (dynamic tag in table.Tags)
                {
                    result.Add(new
                    {
                        plc       = plcName,
                        table     = (string)table.Name,
                        name      = (string)tag.Name,
                        dataType  = (string)tag.DataTypeName,
                        address   = SafeString(() => (string)tag.LogicalAddress),
                        comment   = SafeString(() => (string)tag.Comment.ToString()),
                    });
                }
            }
            foreach (dynamic subGroup in group.Groups)
                CollectTagsFromGroup(subGroup, tableName, result, plcName);
        }

        // ── Hardware Config ──────────────────────────────────────────────────

        /// <summary>
        /// Returns the hardware device tree for the project —
        /// all devices, their device items, module types, and addresses.
        /// </summary>
        public object GetHardwareConfig()
        {
            try
            {
                AssertProject();
                var devices = new List<object>();

                foreach (dynamic device in _tia.Project!.Devices)
                {
                    var items = new List<object>();
                    CollectDeviceItems(device.DeviceItems, items);

                    devices.Add(new
                    {
                        name      = (string)device.Name,
                        typeId    = SafeString(() => (string)device.TypeIdentifier),
                        items
                    });
                }

                // Also include device groups (e.g. unassigned devices, subnets)
                foreach (dynamic dg in _tia.Project!.DeviceGroups)
                {
                    foreach (dynamic device in dg.Devices)
                    {
                        var items = new List<object>();
                        CollectDeviceItems(device.DeviceItems, items);
                        devices.Add(new
                        {
                            name   = (string)device.Name,
                            group  = (string)dg.Name,
                            typeId = SafeString(() => (string)device.TypeIdentifier),
                            items
                        });
                    }
                }

                return new { success = true, deviceCount = devices.Count, devices };
            }
            catch (Exception ex) { return Error(ex); }
        }

        private void CollectDeviceItems(dynamic deviceItems, List<object> result)
        {
            foreach (dynamic item in deviceItems)
            {
                result.Add(new
                {
                    name      = (string)item.Name,
                    typeId    = SafeString(() => (string)item.TypeIdentifier),
                    position  = SafeString(() => (string)item.PositionNumber.ToString()),
                    address   = SafeString(() => (string)item.GetAttribute("StartAddress")?.ToString()),
                });

                // Recurse into sub-items (e.g. submodules)
                try { CollectDeviceItems(item.DeviceItems, result); } catch { /* leaf node */ }
            }
        }

        // ── Project Metadata ─────────────────────────────────────────────────

        /// <summary>
        /// Returns project-level metadata: name, author, version, dates, description.
        /// </summary>
        public object GetProjectMetadata()
        {
            try
            {
                AssertProject();
                dynamic project = _tia.Project!;

                return new
                {
                    success      = true,
                    name         = SafeString(() => (string)project.Name),
                    path         = SafeString(() => (string)project.Path.ToString()),
                    author       = SafeString(() => (string)project.Author),
                    version      = SafeString(() => (string)project.ProjectVersion?.ToString()),
                    comment      = SafeString(() => (string)project.Comment?.ToString()),
                    created      = SafeString(() => ((DateTime)project.CreationTime).ToString("yyyy-MM-dd HH:mm")),
                    lastModified = SafeString(() => ((DateTime)project.LastModified).ToString("yyyy-MM-dd HH:mm")),
                    tiaVersion   = _tia.InstalledVersion
                };
            }
            catch (Exception ex) { return Error(ex); }
        }

        // ── Block Comments ────────────────────────────────────────────────────

        /// <summary>
        /// Returns the title, block comment, and all network/rung comments
        /// from a named block. Works for SCL, LAD, FBD, and STL blocks.
        /// </summary>
        public object GetBlockComments(string plcName, string blockName)
        {
            try
            {
                AssertProject();

                foreach (var plcSw in GetTargetPlcs(plcName))
                {
                    var block = FindBlock(plcSw.BlockGroup, blockName);
                    if (block == null) continue;

                    // Export to XML and parse comments from the SIMATIC ML format
                    var tempFile = Path.Combine(
                        Path.GetTempPath(), $"{blockName}_comments_{Guid.NewGuid():N}.xml");

                    try
                    {
                        block.Export(new FileInfo(tempFile), 0 /* ExportOptions.None */);
                        var xml = File.ReadAllText(tempFile);
                        var comments = ExtractCommentsFromSimaticXml(xml, blockName);
                        return new { success = true, plc = GetPlcName(plcSw), block = blockName, comments };
                    }
                    finally
                    {
                        if (File.Exists(tempFile)) File.Delete(tempFile);
                    }
                }

                return new { success = false, message = $"Block '{blockName}' not found." };
            }
            catch (Exception ex) { return Error(ex); }
        }

        /// <summary>
        /// Extracts human-readable comment strings from a TIA Portal SIMATIC ML XML export.
        /// Looks for Title, Comment, and NetworkComment elements.
        /// </summary>
        private static object ExtractCommentsFromSimaticXml(string xml, string blockName)
        {
            var result = new
            {
                title    = ExtractXmlValue(xml, "Title"),
                comment  = ExtractXmlValue(xml, "Comment"),
                networks = ExtractNetworkComments(xml)
            };
            return result;
        }

        private static string ExtractXmlValue(string xml, string tagName)
        {
            var open  = $"<{tagName}>";
            var close = $"</{tagName}>";
            var start = xml.IndexOf(open,  StringComparison.OrdinalIgnoreCase);
            if (start < 0) return "";
            start += open.Length;
            var end = xml.IndexOf(close, start, StringComparison.OrdinalIgnoreCase);
            return end < 0 ? "" : xml[start..end].Trim();
        }

        private static List<string> ExtractNetworkComments(string xml)
        {
            var comments = new List<string>();
            var searchFrom = 0;
            const string tag = "<NetworkComment>";
            const string endTag = "</NetworkComment>";

            while (true)
            {
                var start = xml.IndexOf(tag, searchFrom, StringComparison.OrdinalIgnoreCase);
                if (start < 0) break;
                start += tag.Length;
                var end = xml.IndexOf(endTag, start, StringComparison.OrdinalIgnoreCase);
                if (end < 0) break;
                var comment = xml[start..end].Trim();
                if (!string.IsNullOrWhiteSpace(comment))
                    comments.Add(comment);
                searchFrom = end + endTag.Length;
            }

            return comments;
        }

        // ── Shared helpers ────────────────────────────────────────────────────

        private void AssertProject()
        {
            if (_tia.Project == null)
                throw new InvalidOperationException(
                    "No project is open. Call TiaOpenProject first.");
        }

        private IEnumerable<dynamic> GetAllPlcSoftware()
        {
            var softwareContainerType = _tia.Asm.GetType("Siemens.Engineering.HW.Features.SoftwareContainer");
            var plcSoftwareType       = _tia.Asm.GetType("Siemens.Engineering.SW.PlcSoftware");

            foreach (dynamic device in _tia.Project!.Devices)
            {
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
                catch { }
            }

            if (plcSwType != null)
            {
                try
                {
                    var sw = CallGetService(item, plcSwType);
                    if (sw?.GetType().Name == "PlcSoftware") return (dynamic)sw;
                }
                catch { }
            }

            return null;
        }

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

        /// <summary>
        /// Safely reads a value that may not be available on all TIA Portal versions.
        /// Returns an empty string instead of throwing.
        /// </summary>
        private static string SafeString(Func<string> getter)
        {
            try { return getter() ?? ""; }
            catch { return ""; }
        }

        private static object Error(Exception ex)
            => new { success = false, error = ex.Message };
    }
}
