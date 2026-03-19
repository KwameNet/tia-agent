// Tools/ReadTools.cs
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using TiaMcpServer.Services;

[McpServerToolType]
public static class ReadTools
{
    [McpServerTool, Description(
        "List all tag tables defined in a PLC. " +
        "Returns table names and tag counts. Use TiaGetTags to read the actual tags.")]
    public static string TiaListTagTables(
        TiaReadService read,
        [Description("PLC device name. Leave empty to query all PLCs.")] string plcName = "")
        => JsonSerializer.Serialize(read.ListTagTables(plcName));

    [McpServerTool, Description(
        "Read all tags from a tag table — name, data type, address, and comment. " +
        "Leave tableName empty to read all tags across all tables in the PLC.")]
    public static string TiaGetTags(
        TiaReadService read,
        [Description("PLC device name.")] string plcName,
        [Description("Tag table name. Leave empty to return all tags in the PLC.")] string tableName = "")
        => JsonSerializer.Serialize(read.GetTags(plcName, tableName));

    [McpServerTool, Description(
        "Read the full hardware configuration of the project — " +
        "all devices, CPU types, I/O modules, rack positions, and addresses.")]
    public static string TiaGetHardwareConfig(TiaReadService read)
        => JsonSerializer.Serialize(read.GetHardwareConfig());

    [McpServerTool, Description(
        "Read project-level metadata: name, author, TIA Portal version, " +
        "creation date, last modified date, and the project description/comment.")]
    public static string TiaGetProjectMetadata(TiaReadService read)
        => JsonSerializer.Serialize(read.GetProjectMetadata());

    [McpServerTool, Description(
        "Read the title, block comment, and all network/rung comments from a block. " +
        "Works for SCL, LAD, FBD, and STL blocks. " +
        "Use this before reading full source to get a quick understanding of block intent.")]
    public static string TiaGetBlockComments(
        TiaReadService read,
        [Description("PLC device name.")] string plcName,
        [Description("Block name, e.g. 'MotorControl'.")] string blockName)
        => JsonSerializer.Serialize(read.GetBlockComments(plcName, blockName));
}
