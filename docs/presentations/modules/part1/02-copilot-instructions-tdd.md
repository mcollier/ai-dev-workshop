---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# Module 2

## Copilot Instructions & TDD
### Red-Green-Refactor with AI

**Duration:** 30 minutes

---

## What Are Copilot Instructions?

`.github/instructions/dotnet.instructions.md` (auto-loads for C# files)  

**Purpose:**
- Repository-wide AI behavior
- Always active (no manual activation)
- Team standards enforcement
- Consistent across all team members

**Think of it as:** A senior developer reviewing every suggestion

---

## Our Copilot Instructions

Key rules encoded:

| Rule | 🔷 .NET |
| ------ | --------- |
| **TDD first** | ✅ Tests before code |
| **Clean Architecture** | ✅ Domain has no deps |
| **DDD patterns** | ✅ Aggregates, VOs |
| **Stack** | ✅ .NET 10, Minimal APIs |
| **Testing** | ✅ xUnit + FakeItEasy |
| **Naming** | ✅ PascalCase / camelCase |

---

## Test-Driven Development (TDD)

```
🔴 RED → ✅ GREEN → ♻️ REFACTOR
```

## Red Phase
Write a **failing test** that defines desired behavior

## Green Phase
Write **minimal code** to make the test pass

## Refactor Phase
**Improve code quality** while keeping tests green

---

## Why TDD with Copilot?

**Traditional concern:** "AI writes code without tests"

**Our approach:**
- Instructions **enforce tests first**
- AI generates test cases from requirements
- Tests are easier to review than implementations
- Validates AI-generated code immediately

**Result:** Higher quality, verified code

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

---

<!-- markdownlint-disable-next-line MD025 -->
# Demo: TDD Workflow

**Watch for:**
- How Copilot suggests test scenarios
- Interface-first design
- Test structure (Arrange-Act-Assert)
- Implementation simplicity
- Refactoring suggestions

**Then:** You'll do it hands-on in Lab 01

---

<!-- markdownlint-disable-next-line MD025 -->
# Hands-On Time

**Lab Guide:** [Lab 01: TDD with Copilot](../../../labs/lab-01-tdd-with-copilot.md)

**Next Module:** [Requirements to Code](03-requirements-to-code.md)

**Previous Module:** [Copilot Features Tour](01-copilot-features-tour.md)
