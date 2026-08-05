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

<style scoped>
section {
  font-size: 20px;
}
</style>

### Individuals

| Free                    | Pro                    | Pro+                   | Max                     |
| ----------------------- | ---------------------- | ---------------------- | ----------------------- |
| $0                      | $10                    | $39                    | $100                    |
| 2,000 completions/month | 1,500 AI credits/month | 7,000 AI credits/month | 20,000 AI credits/month |

### Business

| Business                                        | Enterprise                        |
| ----------------------------------------------- | --------------------------------- |
| $19                                             | $39                               |
| 1,900 AI credits/month                          | 3,900 AI credits/month            |
| Broad governance, IP indemnity and data privacy | Broad model access & higher usage |

_Business and Enterprise use pooled credits._
_Credit usage varies by model (1 AI credit = $0.01 USD)._

---

## Claude Code Pricing

<style scoped>
section {
  font-size: 20px;
}
</style>

### Individuals

| Free           | Pro                                         | Max 5x           | Max 20x           |
| -------------- | ------------------------------------------- | ---------------- | ----------------- |
| $0             | $17                                         | $100             | $200              |
| No Claude Code | Access to Claude Code, Cowork, Design, etc. | 5x more than Pro | 20x more than Pro |

### Business

| Team - Standard | Team - Premium              | Enterprise     |
| --------------- | --------------------------- | -------------- |
| $20             | $100                        | $20 + API rate |
|                 | 5x more usage than Standard |                |

_5 hour rolling usage limit window_

### API
- Rates vary by model
- Input, Output, and Prompt caching per million tokens (e.g., $5/MTok)

---

## Five AI-assisted development experiences

### GitHub Copilot in the IDE: AI layer throughout the editor

- **Inline completions**: ghost-text suggestions as you type
- **Next-edit suggestions**: predicts where and what you may edit next
- **Inline chat**: ask for a change directly at the cursor or selected code
- **Plan, Ask, and Agent modes**: Determine approach, topic discussion, and multi-step task execution

💡_AI integrated into the everyday editing experience._

---

### GitHub Copilot CLI: terminal native coding agent

`copilot`

- Interactive, Plan, or Autopilot (experimental) mode support
- Slash (`/`) commands to invoke direct the session or invoke skills
- Connect to VS Code to editor selection, visual diffs, and session sharing.
- More feature rich than GitHub Copilot IDE extension

💡_Copilot agent centered in the terminal, with strong GitHub integration._

---

### GitHub Copilot App: agent-driven development with strong GitHub integration

- Parallel agent workstreams (each session in its own git worktree)
- End-to-end GitHub integration (issues, PRs, repo search, CI results) without switching tools
- Use canvases to create custom interfaces to collaborate or view info

💡_A platform for supervising AI-powered software delivery._

---

### Claude Code in the IDE: Claude Code with a graphical editor interface

- Chat panel with editor selection context
- Primarily agent-first workflows
- VS Code, Cursor (other VS Code forks), and JetBrains. No Visual Studio extension.
- Subset of Claude Code CLI features (limited commands & skills & no tab completion)
- Bundles own copy of CLI, but need standalone CLI to run in terminal.

💡_A coding agent displayed inside the IDE._

---

### Claude Code CLI: a terminal-first coding agent

`claude`

- Powerfull, terminal-and-agent-first experience
- Slash (`/`) commands to invoke direct the session or invoke skills
- More features than IDE extension
- Started here and added IDE support.

💡_A terminal-first software engineering agent._

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

- **Copilot IDE**: primarily prompt shortcuts (`/tests`, `/explain`)
- **Copilot CLI**: agent-control and development-workflow commands (`/research`, `/pr`, `/review`)
- **Copilot App**: agent, session and workflow management (`/af`, `/create-canvas`, `/orchestrate`)
- **Claude Code**: mixture of agent controls and reusable skills (`/compact`, `/model`, `/skill-name`)

_Note: slash commands vary by tool and available extensions_

**Try it now:** Type `/help` or `/` in GitHub Copilot or Claude Code

---

## Chat Participants

Copilot Chat in VS Code provides _participants_ which provide specialized extension-provided assistance.
`@vscode`, `@terminal`, `@github`

Claude Code automatically routes tasks to a subagent, skill, or MCP server based on installed plugins.

_Extensions may add additional chat participants._

---

## Context Variables

Reference specific context:

### GitHub Copilot IDE

- `@participant`, `/command`, #context (`#file`, `#codebase`, `#selection`)
- Attach screenshots or VS Code integrated browser

### GitHub Copilot CLI or Claude Code

- `/skill`, `@file-or-directory` plus agent discovered context

---

## Tokens

A token is the basic chunk of text an AI model uses to understand and generate language.

- **Input tokens**: your prompt, instructions, history, etc.
- **Output tokens**: the model's response

One token is ~4 characters of English text (3/4th of a word; 100 tokens ~= 75 words).

---

## Context Window

- All the text the model can reference when generating a response.
- Always-on instructions, your prompt, additional context, MCP tools, skills, it's own responses, etc. contribute.
- Size varies by model

_Keep context window as small as possible. The larger the window, the "dumber" responses._

_Use `/context` in Claude Code or GitHub Copilot to check tokens in session context window._

---

## Cache

- Prompt caching avoids repeatedly processing the same instructions and context, reducing response time and token cost.
- Stable context should appear first with changes at the end.
- TTL varies by model and provider

### Breaking the cache

- Switching models mid-session
- Adding new tools
- Going beyond the TTL

---

## Quick Practice (5 min)

**Try these:**

1. Open `TaskManager.Domain/Tasks/Task.cs`
2. Use `/explain` on the Task class
3. Try `#codebase` with: "What's the architecture pattern?"
4. Use `#file` with: "Suggest improvements for #file:Task.cs"

**Goal:** Get comfortable with your assistant's features

---

## Ready for TDD

**Next Module:** [Copilot Instructions & TDD](02-copilot-instructions-tdd.md)

**Previous Module:** [Kickoff & Setup](00-kickoff-and-setup.md)
