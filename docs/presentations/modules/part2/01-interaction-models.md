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

|           | VS Code Copilot            | Copilot CLI                                                                                           | Claude Code                                                                                           |
| --------- | -------------------------- | ----------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------- |
| **Ask**   | Ask mode (explicit toggle) | No toggle — default session answers. (`/ask` or `/btw` for quick side question w/o adding to history. | No toggle — default session answers. (`/ask` or `/btw` for quick side question w/o adding to history. |
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

## Plan Mode: When to Use

✅ **Use when:**
- You want to understand the approach before any code is written
- Requirements need clarifying before implementation
- Complex changes spanning multiple files
- You want to validate the strategy first

❌ **Don't use when:**
- You need a quick direct answer
- You're ready to execute immediately

💡_Try `/research` (GitHub Copilot CLI) or `/deep-research` (Claude Code) to execute a fan-out web search and create a report._

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

**Lab Guide:** [Lab 05: Interaction Models](../../../labs/lab-05-interaction-models.md)

**Next Module:** [Skills & Customization](02-skills-customization.md)

**Previous Module:** [Welcome & Recap](00-welcome-recap.md)
