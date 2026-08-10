---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 3

## Custom Agents
### Workflows with Tool Access

**Duration:** 25 minutes

---

## What Are Custom Agents?

**Named chat participants** with specific roles

- ![w:16](../../../images/githubcopilot.svg) Selectable from agent dropdown (VS Code IDE) or `/agent` (Copilot CLI)
- ![w:16](../../../images/claude-color.svg) Claude Code: **subagents** are auto-routed by task description
- Role-based AI personas (e.g., Architecture Reviewer)
- Defined scope and constraints
- Structured, consistent outputs
- Encode team knowledge

---

## Mental Model: The Specialist

```text
Standard Agent = General AI Assistant

Custom Agent = Domain Expert Consultant
```

You wouldn't ask a general assistant to:
- Review architecture → Ask an architect
- Plan testing → Ask a QA specialist  
- Generate backlog → Ask a product analyst

**Custom agents ARE those specialists**

---

## The Complete Hierarchy

| Feature         | Prompts  | Instructions | Skills                         | Agents                                                                                                                                                                                                     |
| --------------- | -------- | ------------ | ------------------------------ | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Scope**       | One-off  | Always-on    | On-demand                      | On-demand                                                                                                                                                                                                  |
| **Invocation**  | Chat     | Automatic    | Slash command (varies by tool) | ![w:14](../../../images/githubcopilot.svg) VS Code `@agent-name`<br/>![w:14](../../../images/githubcopilot.svg) Copilot CLI `/agent`<br/>![w:14](../../../images/claude-color.svg) Claude Code auto-routed |
| **Tool Access** | ❌       | ❌           | ❌                             | ✅                                                                                                                                                                                                         |
| **Purpose**     | Question | Guardrails   | Knowledge                      | Workflow                                                                                                                                                                                                   |

---

## When to Use Custom Agents

✅ **Use agents for:**
- Repeated **workflows** (reviews, planning)
- Tasks requiring **file/codebase access**
- **Multi-step orchestration**
- Validation and review tasks

❌ **Don't use agents for:**
- Template generation (use Skills)
- Simple questions
- Always-on rules (use Instructions)
- Knowledge without actions

---

## Workshop Agents

## Architecture Reviewer
Reviews code for Clean Architecture & DDD compliance

## Backlog Generator
Creates user stories with acceptance criteria

## Test Strategist
Proposes comprehensive test strategies

---

## Guided Exercise

**Try the Architecture Reviewer agent:**

1. Open ![w:16](../../../images/githubcopilot.svg) Copilot or ![w:16](../../../images/claude-color.svg) Claude Code
2. Select "Architecture Reviewer" (dropdown or `/agent` in Copilot, or ask Claude Code to use it)
3. Prompt: "Review the Task domain model"
4. Compare to the default agent's output

**Observe:**
- Structured format
- Consistency
- Depth of analysis

---

## Key Insight

> **Instructions** = Guardrails  
> **Skills** = Knowledge base  
> **Agents** = Specialists you consult  

All three work together!

---

<!-- markdownlint-disable-next-line MD025 -->
# Hands-On Time

**Lab Guide:** [Lab 07: Custom Agents Intro](../../../labs/lab-07-custom-agents-intro.md)

**Next Module:** [Workflow Agents](04-workflow-agents.md)

**Previous Module:** [Skills & Customization](02-skills-customization.md)
