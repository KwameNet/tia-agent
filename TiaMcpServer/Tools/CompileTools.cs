using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using TiaMcpServer.Services;

[McpServerToolType]
public static class CompileTools
{
    [McpServerTool, Description("Compile the specified PLC and return all compiler messages including errors and warnings. Leave plcName empty to compile all PLCs in the project.")]
    public static string TiaCompile(
        TiaService tia,
        [Description("Name of the PLC to compile. Leave empty to compile all PLCs.")] string plcName = "")
        => JsonSerializer.Serialize(tia.Compile(plcName));
}
