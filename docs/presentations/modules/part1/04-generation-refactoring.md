---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 4

## Code Generation & Refactoring
### Scaffolding and Modernization

**Duration:** 45 minutes

---

## Two Workflows

### 1. Generate from Scratch
- Complete CRUD endpoints
- Following Clean Architecture
- With query handlers (CQRS)

### 2. Refactor Legacy Code
- Modernize old patterns
- Apply Object Calisthenics
- Improve testability

**Pick your tool:** VS Code Copilot Chat, Copilot CLI (`copilot`), or Claude Code CLI (`claude`) — same workflows, different interface.

---

<style scoped>
section {
  font-size: 24px;
}
</style>

## Lab 03: Part A - Generation

**Generate:**
- `GET /tasks/{id}` - Retrieve single task
- `PUT /tasks/{id}` - Update task
- `DELETE /tasks/{id}` - Delete task

**Using:**
- ![w:16](../../../images/githubcopilot.svg) VS Code Copilot Chat: `#codebase`, `#file`
- ![w:16](../../../images/githubcopilot.svg) Copilot CLI or ![w:16](../../../images/claude-color.svg) Claude Code CLI: `@file-or-directory` + agent auto-discovery
- Minimal API patterns
- CQRS queries
- Integration tests

**Time:** 20 minutes

---

## CQRS Pattern

**Command-Query Responsibility Segregation**

## Commands
- Change state
- Example: `CreateTaskCommand`, `UpdateTaskCommand`

## Queries
- Read state
- Example: `GetTaskByIdQuery`, `GetAllTasksQuery`

**Benefit:** Clear separation of read/write concerns

---

<style scoped>
section {
  font-size: 24px;
}
</style>

## Lab 03: Part B - Refactoring

**Legacy code:**
- `LegacyTaskProcessor.cs`

**Problems:**
- Nested if statements
- Abbreviations
- No guard clauses
- Poor testability

**Your task:** Analyze, plan and implement improvements to the code.

**Principles:** Object Calisthenics

💡 _Planning workflow are built for exactly this "analyze → plan → implement" flow — ask for a plan first, review it, then have the agent execute it._

---

## Object Calisthenics (Light)

**Key rules:**
- One level of indentation per method
- Don't use `else` - use guard clauses
- No abbreviations
- Wrap primitives in meaningful types
- Small methods with clear names

---

**Example:**
```csharp
// ❌ Before
if (task != null) {
    if (task.Status == "Active") {
        // logic
    }
}

// ✅ After
if (task == null) return;
if (task.Status != "Active") return;
// logic
```

---

<!-- markdownlint-disable-next-line MD025 -->
# Hands-On Time

**Lab Guide:**
- [Lab 03: Generation & Refactoring (.NET)](../../../labs/lab-03-generation-and-refactoring.md)

**Next Module:** [Testing & Documentation](05-testing-documentation.md)

**Previous Module:** [Requirements to Code](03-requirements-to-code.md)
