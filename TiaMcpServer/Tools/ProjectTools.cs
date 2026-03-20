using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using TiaMcpServer.Services;

[McpServerToolType]
public static class ProjectTools
{
    [McpServerTool, Description("Attach to a TIA Portal instance that is already running with a project open. Use this instead of TiaOpenProject when TIA Portal is already open on the desktop.")]
    public static string TiaAttach(TiaService tia)
        => JsonSerializer.Serialize(tia.AttachToRunning());

    [McpServerTool, Description("Open a TIA Portal project file. Accepts .ap15, .ap16, .ap17, .ap18, .ap19, or .ap20.")]
    public static string TiaOpenProject(
        TiaService tia,
        [Description("Absolute Windows path to the project file.")] string projectPath)
        => JsonSerializer.Serialize(tia.OpenProject(projectPath));

    [McpServerTool, Description("Save the currently open TIA Portal project.")]
    public static string TiaSaveProject(TiaService tia)
        => JsonSerializer.Serialize(tia.SaveProject());

    [McpServerTool, Description("Close the currently open project, optionally saving first.")]
    public static string TiaCloseProject(
        TiaService tia,
        [Description("Whether to save before closing. Default: true.")] bool save = true)
        => JsonSerializer.Serialize(tia.CloseProject(save));

    [McpServerTool, Description("List all PLC devices in the open project.")]
    public static string TiaListPlcs(TiaService tia)
        => JsonSerializer.Serialize(tia.ListPlcs());

    [McpServerTool, Description("List all installed TIA Portal versions detected on this machine.")]
    public static string TiaListInstalledVersions(TiaService tia)
        => JsonSerializer.Serialize(tia.GetInstalledVersions());

    [McpServerTool, Description("List all currently running TIA Portal processes that can be attached to via TiaAttach.")]
    public static string TiaListRunningInstances(TiaService tia)
        => JsonSerializer.Serialize(tia.ListRunningInstances());

    [McpServerTool, Description(
        "Attach to an already-running TIA Portal instance (one that was opened by the user, not by this tool). " +
        "Call TiaListRunningInstances first if you need to pick a specific process. " +
        "If only one instance is running the processIndex can be omitted.")]
    public static string TiaAttach(
        TiaService tia,
        [Description("Zero-based index of the process from TiaListRunningInstances. Omit when only one instance is running.")] int? processIndex = null)
        => JsonSerializer.Serialize(tia.AttachToRunningInstance(processIndex));
}
