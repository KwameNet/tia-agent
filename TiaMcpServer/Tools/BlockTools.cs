using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using TiaMcpServer.Services;

[McpServerToolType]
public static class BlockTools
{
    [McpServerTool, Description("List all program blocks in the open project. Optionally filter by PLC name.")]
    public static string TiaListBlocks(
        TiaService tia,
        [Description("Name of the PLC to list blocks from. Leave empty to list blocks from all PLCs.")] string plcName = "")
        => JsonSerializer.Serialize(tia.ListBlocks(plcName));

    [McpServerTool, Description("Export the SCL source code of a specific block from the project.")]
    public static string TiaGetBlockSource(
        TiaService tia,
        [Description("Name of the PLC containing the block.")] string plcName,
        [Description("Name of the block to export.")] string blockName,
        [Description("Type of block: FB, FC, OB, or DB.")] string blockType)
        => JsonSerializer.Serialize(tia.GetBlockSource(plcName, blockName, blockType));

    [McpServerTool, Description("Import an SCL block into the specified PLC. Overwrites an existing block with the same name.")]
    public static string TiaImportSclBlock(
        TiaService tia,
        [Description("Name of the target PLC.")] string plcName,
        [Description("Raw SCL source code to import (no markdown, complete block only).")] string sclCode,
        [Description("Block group path to import into. Default: 'Program blocks'.")] string blockGroup = "Program blocks")
        => JsonSerializer.Serialize(tia.ImportSclBlock(plcName, blockGroup, sclCode));
}
