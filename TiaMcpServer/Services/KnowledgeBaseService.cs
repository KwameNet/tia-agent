using System.Text.RegularExpressions;

namespace TiaMcpServer.Services;

/// <summary>
/// Loads the SCL knowledge base from markdown files and provides full-text search.
/// Files are loaded once at startup and indexed for fast keyword lookup.
/// </summary>
public sealed class KnowledgeBaseService
{
    private readonly List<KnowledgeEntry> _entries = new();
    private bool _loaded;

    /// <summary>
    /// A single knowledge base entry parsed from a markdown file.
    /// </summary>
    public sealed class KnowledgeEntry
    {
        public string Name { get; init; } = "";
        public string Category { get; init; } = "";
        public string FilePath { get; init; } = "";
        public string Content { get; init; } = "";
        /// <summary>Lowercased content for search matching.</summary>
        public string ContentLower { get; init; } = "";
        /// <summary>Lowercased name for search matching.</summary>
        public string NameLower { get; init; } = "";
    }

    /// <summary>
    /// Result returned from a search query.
    /// </summary>
    public sealed class SearchResult
    {
        public string Name { get; init; } = "";
        public string Category { get; init; } = "";
        public string Snippet { get; init; } = "";
        public double Score { get; init; }
    }

    public KnowledgeBaseService()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        if (_loaded) return;

        // Resolve knowledge-base path relative to the executing assembly
        var baseDir = AppContext.BaseDirectory;
        // Walk up to find tia-agent root, then into docs/knowledge-base
        var dir = FindKnowledgeBaseDir(baseDir);
        if (dir == null)
        {
            // Fallback: try relative to working directory
            dir = FindKnowledgeBaseDir(Directory.GetCurrentDirectory());
        }

        if (dir == null || !Directory.Exists(dir))
        {
            _loaded = true;
            return;
        }

        foreach (var file in Directory.GetFiles(dir, "*.md", SearchOption.AllDirectories))
        {
            // Skip the INDEX.md file
            if (Path.GetFileName(file).Equals("INDEX.md", StringComparison.OrdinalIgnoreCase))
                continue;

            var content = File.ReadAllText(file);
            var name = ExtractName(content, file);
            var category = ExtractCategory(content, file);

            _entries.Add(new KnowledgeEntry
            {
                Name = name,
                Category = category,
                FilePath = file,
                Content = content,
                ContentLower = content.ToLowerInvariant(),
                NameLower = name.ToLowerInvariant()
            });
        }

        _loaded = true;
    }

    private static string? FindKnowledgeBaseDir(string startDir)
    {
        // Try to find docs/knowledge-base by walking up
        var current = startDir;
        for (int i = 0; i < 10; i++)
        {
            var candidate = Path.Combine(current, "docs", "knowledge-base");
            if (Directory.Exists(candidate))
                return candidate;

            // Also check if we're in TiaMcpServer and need to go up to tia-agent
            candidate = Path.Combine(current, "tia-agent", "docs", "knowledge-base");
            if (Directory.Exists(candidate))
                return candidate;

            var parent = Directory.GetParent(current);
            if (parent == null) break;
            current = parent.FullName;
        }
        return null;
    }

    private static string ExtractName(string content, string filePath)
    {
        // Try to get name from the first # heading
        var match = Regex.Match(content, @"^#\s+(.+)$", RegexOptions.Multiline);
        if (match.Success)
            return match.Groups[1].Value.Trim();

        // Fallback to filename
        return Path.GetFileNameWithoutExtension(filePath).Replace("-", " ");
    }

    private static string ExtractCategory(string content, string filePath)
    {
        // Try to get category from frontmatter
        var match = Regex.Match(content, @"\*\*Category:\*\*\s+(.+?)(?:\s{2,}|$)", RegexOptions.Multiline);
        if (match.Success)
            return match.Groups[1].Value.Trim();

        // Fallback to parent directory name
        var parentDir = Path.GetFileName(Path.GetDirectoryName(filePath) ?? "");
        return parentDir.Replace("-", " ");
    }

    /// <summary>
    /// Search the knowledge base using keyword matching.
    /// Returns results ranked by relevance (name match > content match).
    /// </summary>
    public List<SearchResult> Search(string query, int maxResults = 5)
    {
        if (string.IsNullOrWhiteSpace(query) || _entries.Count == 0)
            return new List<SearchResult>();

        var queryLower = query.ToLowerInvariant();
        var queryTerms = queryLower.Split(new[] { ' ', ',', ':', ';' },
            StringSplitOptions.RemoveEmptyEntries);

        var scored = new List<(KnowledgeEntry entry, double score)>();

        foreach (var entry in _entries)
        {
            double score = 0;

            // Exact name match (highest priority)
            if (entry.NameLower == queryLower)
                score += 100;

            // Name contains the full query
            else if (entry.NameLower.Contains(queryLower))
                score += 50;

            // Per-term scoring
            foreach (var term in queryTerms)
            {
                // Term appears in name
                if (entry.NameLower.Contains(term))
                    score += 20;

                // Term appears in content — count occurrences (capped)
                int occurrences = CountOccurrences(entry.ContentLower, term);
                score += Math.Min(occurrences * 2, 20);
            }

            if (score > 0)
                scored.Add((entry, score));
        }

        return scored
            .OrderByDescending(x => x.score)
            .Take(maxResults)
            .Select(x => new SearchResult
            {
                Name = x.entry.Name,
                Category = x.entry.Category,
                Score = x.score,
                Snippet = ExtractSnippet(x.entry.Content, queryTerms)
            })
            .ToList();
    }

    /// <summary>
    /// Get the full content of a specific instruction by name.
    /// </summary>
    public string? GetInstruction(string name)
    {
        var nameLower = name.ToLowerInvariant();
        var entry = _entries.FirstOrDefault(e => e.NameLower == nameLower)
                 ?? _entries.FirstOrDefault(e => e.NameLower.Contains(nameLower));
        return entry?.Content;
    }

    /// <summary>
    /// List all available categories and their instruction counts.
    /// </summary>
    public Dictionary<string, List<string>> ListCategories()
    {
        return _entries
            .GroupBy(e => e.Category)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Name).OrderBy(n => n).ToList());
    }

    public int EntryCount => _entries.Count;

    private static int CountOccurrences(string text, string term)
    {
        int count = 0;
        int index = 0;
        while ((index = text.IndexOf(term, index, StringComparison.Ordinal)) != -1)
        {
            count++;
            index += term.Length;
        }
        return count;
    }

    private static string ExtractSnippet(string content, string[] queryTerms, int snippetLength = 500)
    {
        // Find the first occurrence of any query term and extract surrounding context
        var contentLower = content.ToLowerInvariant();
        int bestIndex = -1;

        foreach (var term in queryTerms)
        {
            int idx = contentLower.IndexOf(term, StringComparison.Ordinal);
            if (idx >= 0 && (bestIndex < 0 || idx < bestIndex))
                bestIndex = idx;
        }

        if (bestIndex < 0)
            bestIndex = 0;

        // Start a bit before the match
        int start = Math.Max(0, bestIndex - 100);
        int end = Math.Min(content.Length, start + snippetLength);

        var snippet = content.Substring(start, end - start).Trim();

        // Clean up: don't start/end mid-word
        if (start > 0) snippet = "..." + snippet.Substring(snippet.IndexOf(' ') + 1);
        if (end < content.Length) snippet = snippet.Substring(0, snippet.LastIndexOf(' ')) + "...";

        return snippet;
    }
}
