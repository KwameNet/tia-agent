using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using TiaMcpServer.Services;

[McpServerToolType]
public static class ProjectTools
{
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
}
