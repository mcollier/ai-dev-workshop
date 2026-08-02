---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 3

## Requirements → Backlog → Code
### Transforming User Stories

**Duration:** 45 minutes

---

## The Challenge

**What you get:**
> "As a user, I want to prioritize my tasks"

**What you need:**
- Acceptance criteria
- Domain model changes
- Application logic
- API endpoints
- Comprehensive tests

**With GitHub Copilot / Claude Code:** Generate all of this systematically

---

## Our Approach

```text
User Story
  ↓
Backlog Item with Acceptance Criteria (Copilot / Claude Code)
  ↓
Domain Model (TDD: Tests first)
  ↓
Application Layer (Commands/Queries)
  ↓
API Endpoint (Minimal API)
  ↓
Integration Tests
```

---

## Lab 02 Overview

**Build:** Task Priority feature

**What you'll create:**
- `Priority` value object (Low, Medium, High)
- `Task` entity with Priority property
- `CreateTaskCommand` with handler
- `POST /tasks` API endpoint
- Full test coverage

**Architecture layers:** Domain → Application → API

---

## Domain-Driven Design Patterns

### Value Objects
- Immutable
- Value equality
- Example: `Priority`, `Address`

### Entities
- Identity-based equality
- Lifecycles
- Example: `Task`, `User`

---

### Aggregates
- Consistency boundaries
- Example: `Task` (aggregate root)

**Copilot knows these patterns** via Instructions

---

## Key Technique: Context Variables

```text
Instead of:
"Create a task entity"

Use:
"Add Priority property to #file:Task.cs following DDD patterns"
```

**Why:** More precise, leverages existing context

---

<!-- markdownlint-disable-next-line MD025 -->
# Hands-On Time

**Lab Guide:** [Lab 02: Requirements to Code](../../../labs/lab-02-requirements-to-code.md)

**Next Module:** [Generation & Refactoring](04-generation-refactoring.md)

**Previous Module:** [Copilot Instructions & TDD](02-copilot-instructions-tdd.md)
