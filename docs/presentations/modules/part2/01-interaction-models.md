---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 1

## Interaction Models
### Ask, Plan, Agent

**Duration:** 25 minutes

---

## Ways to Interact with AI Coding Tools

## ❓ Ask Mode
**Purpose:** Learning, exploration, explanation  
**Result:** Answers, guidance (no changes)

## 📑 Plan Mode
**Purpose:** Design approach + gather requirements before code generation  
**Result:** Structured plan awaiting your approval

## 🤖 Agent Mode
**Purpose:** Multi-step, repository-level workflows  
**Result:** Planned changes with human checkpoints

---

<style scoped>
section {
  font-size: 24px;
}
</style>

## Same Concepts, Different Tools

|           | ![w:18](../../../images/githubcopilot.svg) VS Code Copilot            | ![w:18](../../../images/githubcopilot.svg) Copilot CLI                                                                                           | ![w:18](../../../images/claude-color.svg) Claude Code                                                                                           |
| --------- | -------------------------- | ----------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------- |
| **Ask**   | Ask mode (explicit toggle) | No toggle — default session answers. (`/ask` or `/btw` for quick side question w/o adding to history) | No toggle — default session answers. (`/ask` or `/btw` for quick side question w/o adding to history) |
| **Plan**  | Plan mode                  | `/plan` or `Shift+Tab` — explicit plan-first mode                                                     | Plan Mode (`Shift+Tab` twice) — explicit                                                              |
| **Agent** | Agent mode                 | Default session executes changes directly; `/autopilot` toggles full autonomy (experimental)          | Default agentic execution — asks permission per action unless auto-approved                           |

---

## Ask Mode: When to Use

✅ **Use when:**
- Understanding code or concepts
- Exploring options
- Learning patterns
- Getting explanations

❌ **Don't use when:**
- You need actual code changes
- Implementing features
- Refactoring across files

---

<style scoped>
section {
  font-size: 26px;
}
</style>

## Plan Mode: When to Use

✅ **Use when:**
- You want to understand the approach before any code is written
- Requirements need clarifying before implementation
- Complex changes spanning multiple files
- You want to validate the strategy first

❌ **Don't use when:**
- You need a quick direct answer
- You're ready to execute immediately

💡_Try `/research` (![w:16](../../../images/githubcopilot.svg) Copilot CLI) or `/deep-research` (![w:16](../../../images/claude-color.svg) Claude Code) to execute a fan-out web search and create a report._
💡_Review the plan with another model family via `/rubber-duck` (![w:16](../../../images/githubcopilot.svg) Copilot)._

---

## Agent Mode: When to Use

✅ **Use when:**
- Multi-file workflows
- Repository-level analysis
- Complex refactoring
- Need plan-execute-review cycle

<!-- ❌ **Don't use when:**
- Simple, quick edits
- Single file changes
- Learning or exploring -->

**Key:** Agent Mode = Human-in-the-loop by design

---

## Demo: Same Task, Three Ways

**Task:** Add Priority property to Task entity

1. **Ask** → Explanation only
2. **Plan** → Requirement gathering + multi-step design
3. **Agent** → Repository-wide analysis and execution

**Observe the differences in:**
- Scope
- Control
- Visibility
- Workflow

---

## Key Takeaway

> Agent Mode is not "better chat"  
> It's a **different execution model**

Use the **right mode** for the **right job**

---

<!-- markdownlint-disable-next-line MD025 -->
# Hands-On Time

**Next Module:** [Skills & Customization](02-skills-customization.md)

**Previous Module:** [Welcome & Recap](00-welcome-recap.md)
