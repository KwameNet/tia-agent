# TiaFlow — TIA Portal Coding Agent

A self-correcting PLC coding agent powered by [Claude Code](https://docs.anthropic.com/en/docs/claude-code) and the Siemens TIA Portal Openness API. Give it a natural-language task, and it generates SCL code, imports it into TIA Portal, compiles, and automatically fixes errors until the code builds cleanly.

## How It Works

1. You describe a PLC task in plain English (e.g. *"Create a motor start/stop FB with a 5-second ramp delay"*)
2. The agent searches the **SCL knowledge base** to look up relevant instructions and syntax
3. It generates valid SCL code and imports it into TIA Portal via the MCP server
4. It compiles and reads the compiler output
5. If there are errors, it reads them, consults the knowledge base, fixes the code, and retries
6. Reports the final result

## Architecture

```
You (natural language prompt)
  │
  ▼
Claude Code ─── LLM (Claude) decides what to do
  │
  │  stdio (MCP protocol)
  ▼
TiaMcpServer (C# MCP server)
  │
  │  Siemens.Engineering.dll (loaded at runtime)
  ▼
TIA Portal Openness API ──▶ TIA Portal (V15.1 – V20)
```

**Key design decisions:**
- The MCP server is a **standalone process** communicating over stdio — no HTTP, no sockets
- `Siemens.Engineering.dll` is **loaded dynamically at runtime** via reflection, so the project builds on any machine even without TIA Portal installed
- The installed TIA Portal version is **auto-detected** from the Windows registry

## Prerequisites

| Requirement | Details |
|---|---|
| **OS** | Windows 10/11 (64-bit) — TIA Openness is Windows-only |
| **TIA Portal** | V15.1 – V20 supported. **Tested and verified on V20.** Earlier versions should work but are not actively tested |
| **TIA Openness** | Enabled in TIA Portal: **Options → Settings → General → Activate Siemens.Engineering API** |
| **.NET SDK** | [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later (builds target .NET Framework 4.8) |
| **Claude Code** | [Install Claude Code](https://docs.anthropic.com/en/docs/claude-code/getting-started) |

## Setup

### 1. Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/tia-agent.git
cd tia-agent
```

### 2. Enable TIA Portal Openness

This is required for any external application to communicate with TIA Portal.

1. Open TIA Portal
2. Go to **Options → Settings → General**
3. Check **Activate Siemens.Engineering API**
4. Restart TIA Portal

You must also be a member of the **Siemens TIA Openness** Windows user group:

1. Open **Computer Management → Local Users and Groups → Groups**
2. Find **Siemens TIA Openness** (created during TIA Portal installation)
3. Add your Windows user to this group
4. **Log out and back in** for the change to take effect

### 3. Build the MCP server

```bash
cd TiaMcpServer
dotnet build -c Release
```

The build will succeed even without TIA Portal installed — the Siemens DLL is loaded at runtime, not at compile time.

### 4. (Optional) Publish as standalone exe

If you prefer running a pre-built executable instead of `dotnet run`:

```bash
dotnet publish -c Release -o publish
```

Then update `.claude/settings.json` to point to the exe:

```json
{
  "mcpServers": {
    "tia-portal": {
      "command": "C:\\full\\path\\to\\tia-agent\\TiaMcpServer\\publish\\TiaMcpServer.exe",
      "args": [],
      "env": {}
    }
  }
}
```

### 5. Open in Claude Code

```bash
cd tia-agent
claude
```

Claude Code will automatically start the MCP server using the config in `.claude/settings.json`.

### 6. Verify the connection

Once Claude Code starts, test the setup:

```
> What TIA Portal versions are installed?
> List the blocks in the open project
```

If the MCP server is running correctly, you'll get real data back from TIA Portal.

## Usage

### Start TIA Portal first

The agent connects to a **running** TIA Portal instance. Always:

1. Start TIA Portal
2. Open your project
3. Then start Claude Code

### Example prompts

```
Create an FC called FC_ValveControl that opens a valve when a start signal is received,
waits 3 seconds, then checks a feedback sensor. If no feedback within 5 seconds, set a fault.
```

```
Read the source of FB_MotorControl and add an emergency stop input that immediately
stops the motor and requires a manual reset.
```

```
Compile PLC_1 and fix any errors.
```

### Version pinning

By default, the highest installed TIA Portal version is used. To pin a specific version, set `TIA_VERSION` in `.claude/settings.json`:

```json
"env": { "TIA_VERSION": "V18" }
```

Accepted formats: `"V18"`, `"v18"`, `"18"` — all equivalent.

## MCP Tools

### Project Management

| Tool | Description |
|---|---|
| `TiaAttach` | Attach to a running TIA Portal instance |
| `TiaOpenProject` | Open a TIA Portal project file (.ap*) |
| `TiaSaveProject` | Save the currently open project |
| `TiaCloseProject` | Close the project and optionally save |
| `TiaListPlcs` | List all PLC devices in the project |
| `TiaListInstalledVersions` | List detected TIA Portal installations |
| `TiaListRunningInstances` | List running TIA Portal processes |

### Block Operations

| Tool | Description |
|---|---|
| `TiaListBlocks` | List all program blocks (FB, FC, OB, DB) |
| `TiaGetBlockSource` | Export the SCL source code of a block |
| `TiaImportSclBlock` | Import SCL code into a PLC (overwrites if exists) |
| `TiaCompile` | Compile a PLC and return compiler messages |

### Read Operations

| Tool | Description |
|---|---|
| `TiaListTagTables` | List all tag tables in a PLC |
| `TiaGetTags` | Get all tags from a tag table |
| `TiaGetHardwareConfig` | Get full hardware configuration tree |
| `TiaGetProjectMetadata` | Get project name, author, version, dates |
| `TiaGetBlockComments` | Extract comments from block XML |

### Hardware

| Tool | Description |
|---|---|
| `TiaConfigureHardware` | Add a hardware device by MLFB order number |

### Knowledge Base

| Tool | Description |
|---|---|
| `TiaSearchDocs` | Search the SCL instruction reference by keyword |
| `TiaGetDocInstruction` | Get full documentation for a specific instruction |
| `TiaListDocs` | List all categories and instructions in the knowledge base |

## Knowledge Base

The `docs/knowledge-base/` directory contains structured markdown files extracted from the official Siemens SCL instruction reference for S7-1200/S7-1500. The agent searches these docs at runtime to verify syntax and parameters before generating code.

**131 instructions** across **18 categories**: timers, counters, math, comparators, move operations, conversions, program control, and more.

To regenerate the knowledge base from the source PDF:

```bash
pip install pdfplumber
python scripts/extract_scl_knowledge.py
```

## Project Structure

```
tia-agent/
├── .claude/
│   └── settings.json              # MCP server configuration
├── TiaMcpServer/
│   ├── Program.cs                 # Server startup and DI registration
│   ├── Services/
│   │   ├── TiaService.cs          # Core TIA Portal operations (dynamic loading)
│   │   ├── TiaReadService.cs      # Read-only project queries
│   │   ├── TiaVersionResolver.cs  # Registry-based version detection
│   │   └── KnowledgeBaseService.cs # SCL docs full-text search
│   ├── Tools/
│   │   ├── ProjectTools.cs        # Project lifecycle tools
│   │   ├── BlockTools.cs          # Block import/export tools
│   │   ├── CompileTools.cs        # Compilation tool
│   │   ├── ReadTools.cs           # Tag/hardware/metadata query tools
│   │   ├── HardwareTools.cs       # Hardware configuration tool
│   │   └── KnowledgeBaseTools.cs  # Knowledge base search tools
│   └── TiaMcpServer.csproj       # .NET Framework 4.8, x64
├── docs/
│   └── knowledge-base/            # SCL instruction reference (131 markdown files)
├── prompts/
│   └── system_prompt.md           # System prompt for SCL generation
├── scripts/
│   └── extract_scl_knowledge.py   # PDF → knowledge base extractor
└── tia-agent.sln
```

## Tested Environment

- TIA Portal **V20** on Windows 11 Pro (64-bit)
- .NET SDK 9.0 / .NET Framework 4.8
- Claude Code with Claude Opus

Earlier TIA Portal versions (V15.1–V19) are supported in code (version-resolved at runtime) but have not been tested. If you encounter issues on an older version, please open an issue.

## Known Limitations

- **Windows only** — TIA Portal Openness is a Windows-only API
- **TIA Portal must be running** — the agent connects to a live instance
- **No failsafe block generation** — never auto-generate or modify F-CPU / safety blocks
- **No download to hardware** — intentionally omitted; downloading requires safety review
- **Single project** — one TIA Portal project open at a time

## License

MIT
