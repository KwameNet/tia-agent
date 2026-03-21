using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using TiaMcpServer.Services;

[McpServerToolType]
public static class KnowledgeBaseTools
{
    [McpServerTool, Description(
        "Search the SCL instruction knowledge base. Use this to look up SCL syntax, " +
        "instruction parameters, usage examples, and behavior details for S7-1200/S7-1500 PLCs. " +
        "Returns ranked results with snippets.")]
    public static string TiaSearchDocs(
        KnowledgeBaseService kb,
        [Description("Search query — instruction name (e.g. 'TON'), keyword (e.g. 'timer delay'), " +
                     "or natural phrase (e.g. 'count up and down').")] string query,
        [Description("Maximum number of results to return. Default: 5.")] int maxResults = 5)
    {
        var results = kb.Search(query, maxResults);
        if (results.Count == 0)
            return JsonSerializer.Serialize(new
            {
                success = true,
                message = $"No results found for '{query}'.",
                availableCategories = kb.ListCategories().Keys
            });

        return JsonSerializer.Serialize(new
        {
            success = true,
            resultCount = results.Count,
            results = results.Select(r => new
            {
                name = r.Name,
                category = r.Category,
                score = r.Score,
                snippet = r.Snippet
            })
        });
    }

    [McpServerTool, Description(
        "Get the full documentation for a specific SCL instruction by name. " +
        "Use this after TiaSearchDocs to read complete details including all parameters, " +
        "examples, and notes.")]
    public static string TiaGetDocInstruction(
        KnowledgeBaseService kb,
        [Description("Instruction name, e.g. 'TON: Generate on-delay' or just 'TON'.")] string name)
    {
        var content = kb.GetInstruction(name);
        if (content == null)
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = $"Instruction '{name}' not found.",
                hint = "Use TiaSearchDocs to find the correct instruction name."
            });

        return JsonSerializer.Serialize(new
        {
            success = true,
            name,
            content
        });
    }

    [McpServerTool, Description(
        "List all categories and instructions available in the SCL knowledge base. " +
        "Use this to discover what documentation is available.")]
    public static string TiaListDocs(KnowledgeBaseService kb)
    {
        var categories = kb.ListCategories();
        return JsonSerializer.Serialize(new
        {
            success = true,
            totalInstructions = categories.Values.Sum(c => c.Count),
            categories = categories.Select(kvp => new
            {
                category = kvp.Key,
                instructionCount = kvp.Value.Count,
                instructions = kvp.Value
            })
        });
    }
}
