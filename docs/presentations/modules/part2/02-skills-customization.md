---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 2

## Skills & Customization Hierarchy
### Four Ways to Customize Your AI Coding Tool

**Duration:** 30 minutes

---

## The Customization Hierarchy

### 4 Types (Specific → General)

1. Prompts      → One-off, in-chat (_move to skills_)
2. Instructions → Always-on guardrails
3. Skills       → Domain knowledge (slash command)
4. Agents       → Workflows with tools (invocation varies by tool, `@agent-name` or `/agent`)

**Key principle:** Use the simplest level that solves your problem

---

## What Are Skills?

**Skills = Domain Expertise Without Tool Access**

- Portable knowledge modules
- Invoked with a slash command (e.g. `/skill-name`)
- Provide templates, patterns, workflows, conventions, reference material
- **No file access** - pure knowledge
- Executable helper scripts (e.g., `.sh`, `.py`)

**Example:** `/test-data-generator`

💡_Keep skills to under 500 lines._

---

<style scoped>
section {
  font-size: 24px;
}
</style>

## Skills & Agents: How They Relate

**Not either/or**: a skill can be invoked directly, _and_ an agent can invoke a skill
mid-task as part of a larger workflow.

| Aspect | Skills | Agents |
| -------- | -------- | -------- |
| **Purpose** | Knowledge & templates | Workflows & actions |
| **Tool Access** | ❌ None | ✅ Read/write files |
| **Invocation** | Slash command, or called by an agent | ![w:14](../../../images/githubcopilot.svg) VS Code: `@agent-name`<br/>![w:14](../../../images/githubcopilot.svg) Copilot CLI: `/agent`<br/>![w:14](../../../images/claude-color.svg) Claude Code: auto-routed |
| **Can invoke the other?** | ❌ No tool access, so a skill can't invoke an agent | ✅ Yes, an agent loads a skill for domain knowledge, then acts on it |
| **Best For** | Patterns, examples | Multi-step tasks; codifying explicit context on how to carry out a process |

---

## When to Use Each Type

**Prompts:** One-time question _(moving to skills)_
**Instructions:** Team coding standards  
**Skills:** Reusable knowledge, conventions, checklists, or multi-step procedure
**Agents:** Orchestrate multi-step workflows

**Decision tree** (what do _you_ invoke directly?):
1. Need to make changes? → Agent (it may invoke a skill along the way)
2. Need templates/knowledge only, no changes? → Skill
3. Always-on rule? → Instructions
~~4. One-off question? → Prompt~~

---

## Slash Commands for Discovery

Every tool has slash (`/`) commands

### Availability varies
- By installed extensions or plugins
- By how you access the tool (CLI vs. IDE)
- By tool - Copilot and Claude Code don't expose identical command names

**Try it:** Type `/` or `/help` in whichever tool you're using; discover what's
actually available in your session

---

## Sharing

Share with colleagues. Build on great work of others.

### Personal
Shared across your projects - create `~/.agents/skills` in your local home directory

### With others

- Use a plugin to share customizations (agents, skills, hooks, MCP server config)
- Add plugins to a marketplace

```bash
# Add the marketplace
/plugin marketplace add my-org/my-marketplace-repo

# Install a specific plugin from the marketplace
/plugin install my-plugin@my-marketplace
```

---

## Recommended Skills

- Lots available on [skills.sh](https://www.skills.sh)
- Awesome Copilot at [https://awesome-copilot.github.com/](https://awesome-copilot.github.com/)
- Matt Pocock's skills at [https://www.aihero.dev/skills](https://www.aihero.dev/skills)
- Addy Osmani's skills at [https://skills.addy.ie/](https://skills.addy.ie/)
- Microsoft's HVE Core at [https://microsoft.github.io/hve-core](https://microsoft.github.io/hve-core/)
- .NET skills at [https://github.com/dotnet/skills/](https://github.com/dotnet/skills/)
- Azure Skills / Plugin at [https://github.com/microsoft/azure-skills](https://github.com/microsoft/azure-skills)

💡_Verify safety before using!_

---

## Key Takeaway

> Skills provide **knowledge**  
> Agents provide **action**  
> An agent often invokes a skill along the way — they compose, not compete

**Most confusion:** Treating skills and agents as either/or  
**Remember:** Does _this specific step_ need to read/write files?
- Yes → Agent (which may invoke a skill internally)
- No → Maybe a Skill (invoked directly)

---

<!-- markdownlint-disable-next-line MD025 -->
# Hands-On Time

**Lab Guide:** [Lab 06: Skills & Customization](../../../labs/lab-06-skills-and-customization.md)

**Next Module:** [Custom Agents Intro](03-custom-agents-intro.md)

**Previous Module:** [Interaction Models](01-interaction-models.md)
