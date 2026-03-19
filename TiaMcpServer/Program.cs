using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;
using TiaMcpServer.Services;

var builder = Host.CreateApplicationBuilder(args);

// One TIA connection shared across all tool calls in a session.
builder.Services.AddSingleton<TiaService>();

// MCP server with stdio transport. Tools are discovered via reflection.
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

var app = builder.Build();
await app.RunAsync();
