using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using TiaMcpServer.Services;

[McpServerToolType]
public static class HardwareTools
{
    [McpServerTool, Description("Add a hardware device to the open project using a Siemens MLFB order number.")]
    public static string TiaConfigureHardware(
        TiaService tia,
        [Description("Name to give the new device (e.g. PLC_1).")] string deviceName,
        [Description("Siemens MLFB order number identifying the hardware module (e.g. 6ES7 515-2AM01-0AB0).")] string mlfb,
        [Description("Rack and slot position in 'rack/slot' format. Default: '0/1'.")] string rackSlot = "0/1")
        => JsonSerializer.Serialize(tia.ConfigureHardware(deviceName, mlfb, rackSlot));
}
