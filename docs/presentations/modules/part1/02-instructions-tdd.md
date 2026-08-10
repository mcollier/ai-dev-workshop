---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 2

## Instructions for GitHub Copilot and Claude Code
### Red-Green-Refactor with AI

**Duration:** 30 minutes

---

## What Are Instructions Files?

<style scoped>
section {
  font-size: 20px;
}
</style>

Each tool has a **repo-wide** layer and a **scoped** layer:

|                                                               | Repo-wide (always loaded)                                                                                                             | Scoped (path/subdirectory)                                                |
| ------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------- |
| **![w:20](../../../images/githubcopilot.svg) GitHub Copilot** | `.github/copilot-instructions.md`                                                                                                     | `.github/instructions/*.instructions.md` (`applyTo` glob, e.g. `**/*.cs`) |
| **![w:20](../../../images/claude-color.svg) Claude Code**     | `CLAUDE.md` (repo root)                                                                                                               | Nested `CLAUDE.md` per subdirectory                                       |
| **Cross-tool standard**                                       | `AGENTS.md` — read natively by Claude Code, and by Copilot CLI/coding agent as an alternative/supplement to `copilot-instructions.md` | —                                                                         |

### Purpose (all tools)
- Repository-wide AI behavior
- Always active (no manual activation)
- Team standards enforcement

👉 [GitHub Copilot custom instruction support matrix](https://docs.github.com/en/copilot/reference/custom-instructions-support)

---

<style scoped>
section {
  font-size: 24px;
}
</style>

## Our AI Instructions

Key rules encoded — same rules, different file depending on tool:

| Rule                   | 🔷 .NET                   |
| ---------------------- | ------------------------- |
| **TDD first**          | ✅ Tests before code      |
| **Clean Architecture** | ✅ Domain has no deps     |
| **DDD patterns**       | ✅ Aggregates, VOs        |
| **Stack**              | ✅ .NET 10, Minimal APIs  |
| **Testing**            | ✅ xUnit + FakeItEasy     |
| **Naming**             | ✅ PascalCase / camelCase |

- ![w:20](../../../images/githubcopilot.svg) Copilot: `copilot-instructions.md` + `dotnet.instructions.md`
- ![w:20](../../../images/claude-color.svg) Claude Code: `CLAUDE.md` (or `AGENTS.md`)
- The content is portable, only the file/loading mechanism differs.

---

<style scoped>
section {
  font-size: 24px;
}
</style>

## Test-Driven Development (TDD)

```text
🔴 RED → ✅ GREEN → ♻️ REFACTOR
```

## Red Phase
Write a **failing test** that defines desired behavior

## Green Phase
Write **minimal code** to make the test pass

## Refactor Phase
**Improve code quality** while keeping tests green

_Same cycle regardless of which AI tool drives it — Copilot or Claude Code._

---

## Why TDD with AI?

**Traditional concern:** "AI writes code without tests"

**Our approach:**
- Instructions file **enforces tests first** (`copilot-instructions.md`/`dotnet.instructions.md` or `CLAUDE.md`/`AGENTS.md`)
- AI generates test cases from requirements
- Tests are easier to review than implementations
- Validates AI-generated code immediately

**Result:** Higher quality, verified code

---

<!-- markdownlint-disable-next-line MD025 -->
# Demo: TDD Workflow

**Watch for:**
- How the instructions file (`copilot-instructions.md`/`dotnet.instructions.md` or `CLAUDE.md`/`AGENTS.md`) is picked up automatically, no matter which tool is used
- How Copilot or Claude Code suggests test scenarios
- Interface-first design
- Test structure (Arrange-Act-Assert)
- Implementation simplicity
- Refactoring suggestions

**Then:** You'll do it hands-on in Lab 01

---

<!-- markdownlint-disable-next-line MD025 -->
# Lab 01 Overview

**Build:** `NotificationService` with TDD

1. Define `INotificationService` interface (RED)
2. Generate comprehensive test suite (RED)
3. Implement `NotificationService` (GREEN)
4. Refactor for quality (REFACTOR)

**Time:** 25 minutes  
**Key learning:** AI accelerates TDD, doesn't bypass it

_Use GitHub Copilot or Claude Code — same lab steps._

---

<!-- markdownlint-disable-next-line MD025 -->
# Hands-On Time

**Lab Guide:** [Lab 01: Test-Driven Development](../../../labs/lab-01-tdd.md)

**Next Module:** [Requirements to Code](03-requirements-to-code.md)

**Previous Module:** [Features Tour](01-features-tour.md)
