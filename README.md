# TiaFlow — TIA Portal Coding Agent

A self-correcting PLC coding agent powered by Claude Code and the TIA Portal Openness API.

## What it does

1. Accepts a natural language task (e.g. *"Create a MotorControl FB with start/stop interlock"*)
2. Generates valid SCL code using Claude
3. Imports the code into TIA Portal via the MCP server
4. Compiles and reads the result
5. Automatically fixes errors and retries until compilation succeeds or escalation is needed
6. Reports the final outcome

## Architecture

```
Claude Code (agent runtime)
        │
        │  stdio (MCP protocol)
        ▼
C# MCP Server (calls TIA Openness API directly)
        │
        │  in-process .NET calls (version resolved at runtime)
        ▼
TIA Portal Openness API  ──▶  TIA Portal (any installed version)
```

## Prerequisites

- Windows 10/11 (64-bit)
- TIA Portal V15.1–V20 installed and licensed
- TIA Portal Openness enabled: **Options → Settings → General → Activate Siemens.Engineering API**
- .NET 6 SDK
- Claude Code

## Startup Sequence

```
1. Start TIA Portal (any version — the server auto-detects it)
2. Open the target project in TIA Portal
3. Open tia-agent/ in Claude Code
4. Ask Claude: "What TIA Portal version is active?"
5. Begin prompting
```

## Version Selection

By default, the highest installed TIA Portal version is used. To pin a version, set `TIA_VERSION` in `.claude/settings.json`:

```json
"env": { "TIA_VERSION": "V18" }
```

Accepted formats: `"V18"`, `"v18"`, `"18"` — all equivalent.

## Build

```bash
cd TiaMcpServer
dotnet build -c Release
```

The build succeeds even without TIA Portal installed — the DLL is loaded at runtime.

## Available Tools

| Tool | Description |
|------|-------------|
| `TiaOpenProject` | Open a TIA Portal project file |
| `TiaSaveProject` | Save the currently open project |
| `TiaCloseProject` | Close the project, optionally saving |
| `TiaListPlcs` | List all PLC devices in the project |
| `TiaListInstalledVersions` | List detected TIA Portal installations |
| `TiaListBlocks` | List all program blocks |
| `TiaGetBlockSource` | Export SCL source of a block |
| `TiaImportSclBlock` | Import SCL code into a PLC |
| `TiaCompile` | Compile a PLC and return messages |
| `TiaConfigureHardware` | Add a hardware device by MLFB |

## Known Limitations

- Windows only (TIA Openness is Windows-only)
- TIA Portal must be running before Claude Code starts
- Never auto-generate or modify failsafe (F-CPU) blocks
- No download to hardware (intentionally omitted — requires safety review)
- Single project at a time
