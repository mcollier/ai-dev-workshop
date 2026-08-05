---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 1

## Features Tour
### Capabilities Overview

**Duration:** 15 minutes

---

## GitHub Copilot Pricing

_Coming soon_

---

## Claude Code Pricing

_Coming soon_

---

## Four AI-assisted development experiences

### GitHub Copilot in the IDE: AI layer throughout the editor

- **Inline completions**: ghost-text suggestions as you type
- **Next-edit suggestions**: predicts where and what you may edit next
- **Inline chat**: ask for a change directly at the cursor or selected code
- **Plan, Ask, and Agent modes**: Determine approach, topic discussion, and multi-step task execution

_AI integrated into the everyday editing experience._

---

### GitHub Copilot CLI: terminal native coding agent

`copilot`

- Agent or Plan mode support
- Slash (`/`) commands to invoke direct the session or invoke skills
- Connect to VS Code to editor selection, visual diffs, and session sharing.
- More feature rich than IDE extension
- Strong GitHub integration

_Copilot agent mode centered in the terminal, with strong GitHub integration._

---

### Claude Code in the IDE: Claude Code with a graphical editor interface

- Chat panel with editor selection context
- Primarily agent-first workflows
- VS Code and JetBrains. No Visual Studio extension.

_A coding agent displayed inside the IDE._

---

### Claude Code CLI: a terminal-first coding agent

`claude`

- Powerfull, terminal-ang-agent-first experience
- Slash (`/`) commands to invoke direct the session or invoke skills
- More features than IDE extension

_A terminal-first software engineering agent._

---

<!-- ## IDE Interaction

### 1. Inline Completions (GitHub Copilot)
- Active while typing. Suggests next lines.
- **Trigger:** Start typing or use `Alt+\` (Windows/Linux) or `Option+\` (Mac)

### 2. Chat Panel (GitHub Copilot and Claude Code)
- Ask questions, explain code
- Get suggestions without editing
- **Trigger:** Extension panel

### 3. Inline Chat (GitHub Copilot)
- Edit code in-place
- **Trigger:** `Ctrl+I` (Windows/Linux) or `Cmd+I` (Mac)

--- -->

## Slash Commands

GitHub Copilot and Claude Code make extensive use of slash (`/`) commands

- **Copilot IDE** slash commands are primarily prompt shortcuts (`/tests`, `/explain`)
- **Copilot CLI** slash commands are agent-control and development-workflow commands (`/research`, `/pr`, `/review`)
- **Claude Code** slash commands are a mixture of agent controls and reusable skills (`/compact`, `/model`, `/skill-name`)

_Note: slash commands vary by tool and available extensions_

**Try it now:** Type `/help` in GitHub Copilot or Claude Code

<!-- Quick shortcuts for common tasks:

| Command     | Purpose           | Example                          |
| ----------- | ----------------- | -------------------------------- |
| `/explain`  | Explain code      | `/explain this function`         |
| `/fix`      | Suggest fixes     | `/fix this error`                |
| `/tests`    | Generate tests    | `/tests for this class`          |
| `/doc`      | Add documentation | `/doc this API`                  |
| `/refactor` | Improve code      | `/refactor to use guard clauses` |

---

## More Slash Commands

| Command        | Purpose            | Example                  |
| -------------- | ------------------ | ------------------------ |
| `/help`        | Show all commands  | `/help`                  |
| `/agents`      | List custom agents | `/agents`                |
| `/skills`      | List skills        | `/skills`                |
| `/init`        | Start new project  | `/init dotnet webapi`    |
| `/create-file` | Create with AI     | `/create-file readme.md` | --> |


---

## Chat Participants

Provide context for better results:

## @vscode
- VS Code features and commands
- "How do I debug tests?"

## @terminal
- CLI commands and troubleshooting
- "How to restore packages?"

_Extensions may add additional chat participants._

---

## Context Variables

Reference specific context:

### #file
- Reference a specific file
- Example: `Explain #file:Task.cs`

### #selection
- Current editor selection
- Example: `Refactor #selection`

### #editor
- Current active file
- Example: `Add tests for #editor`

---

## Quick Practice (5 min)

**Try these:**

1. Open `TaskManager.Domain/Tasks/Task.cs`
2. Use `/explain` on the Task class
3. Try `#codebase` with: "What's the architecture pattern?"
4. Use `#file` with: "Suggest improvements for #file:Task.cs"

**Goal:** Get comfortable with Copilot's features

---

## Ready for TDD

**Next Module:** [Copilot Instructions & TDD](02-copilot-instructions-tdd.md)

**Previous Module:** [Kickoff & Setup](00-kickoff-and-setup.md)
