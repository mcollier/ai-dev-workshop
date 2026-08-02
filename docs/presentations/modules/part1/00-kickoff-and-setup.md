---
marp: true
theme: default
paginate: true
backgroundColor: #fff
---

# GitHub Copilot for .NET Development

## AI-Assisted Development Fundamentals

**Duration:** ~3 Hours  
**Format:** Instructor-led, hands-on  
**Part:** 1 of 2

---

## Welcome to AI-Assisted Development

### What You'll Learn Today

- Use **instructions** for team-wide consistency
- Apply **Test-Driven Development** with AI assistance
- Transform **requirements into working code**
- Generate and **refactor APIs** following Clean Architecture
- Create **comprehensive tests and documentation**
- Follow **conventional commits** and AI-generated PR descriptions

---

## The only constant is change

- GitHub Copilot is available in multiple IDEs, [github.com/copilot](https://github.com/copilot) , CLI, and mobile
- Claude Code is available in VS Code and JetBrains, CLI, Claude desktop, or [claude.ai/code](https://claude.ai/code)
- What you see in Visual Studio may differ from VS Code or other tools
  - See [Copilot feature matrix](https://docs.github.com/en/copilot/reference/copilot-feature-matrix) for version details
- AI-assisted software engineering tools and practices are evolving quickly — expect changes and differences
- Training content reflects current features; some may vary by tool
- Embrace a growth mindset and have fun!

---

## Why This Workshop Matters

**Traditional Development:**
- Manual test writing
- Repetitive CRUD code
- Documentation debt
- Inconsistent patterns across team

**With AI-assisted development:**
- AI generates tests from specifications
- Accelerated implementation
- Documentation alongside code
- Encoded team standards

---

## Prerequisites Check

✅ **GitHub Copilot** - Active subscription  _or_
✅ **Claude Code** - Active subscription  
✅ **VS Code** - Latest stable version  
✅ **Git** - Basic familiarity  
✅ **.NET 10 SDK** - `dotnet --version`  
✅ **C# Experience** - Comfortable with syntax

```bash
# Common
git --version
code --version
claude --version

# .NET
dotnet --version    # Should show 10.x.x
```

---

## Repository Structure

```bash
TaskManager.sln
├── src/
│   ├── TaskManager.Domain/         # Business logic
│   ├── TaskManager.Application/    # Use cases
│   ├── TaskManager.Infrastructure/ # Data access
│   └── TaskManager.Api/            # Minimal API
└── tests/
    ├── TaskManager.UnitTests/
    └── TaskManager.IntegrationTests/
```

**Architecture:** Clean Architecture + DDD  
**Testing:** xUnit + FakeItEasy  
**API:** .NET 10 Minimal APIs

---

## Today's Journey

```text
0. Kickoff & Setup (15 min)
1. Copilot / Claude Code Features Tour (15 min)
2. Instructions & TDD (30 min)
3. Requirements → Code (45 min)
4. Code Generation & Refactoring (45 min)
5. Testing & Documentation (15 min)
6. Wrap-Up & Discussion (15 min)
```

**Total:** ~3 hours with hands-on labs

---

<!-- _class: lead -->

<!-- markdownlint-disable-next-line MD025 -->
# Module 0: Setup & Environment

## Getting Ready

**Duration:** 15 minutes

---

## Clone and Branch

```bash
git clone https://github.com/mcollier/ai-dev-workshop.git
cd ai-dev-workshop

# Create your personal branch
git checkout main
git pull
git checkout -b your-name-workshop
```

**Important:** Work on your own branch to avoid conflicts

---

## Verify Environment

```bash
dotnet --version
dotnet build
dotnet test
```

**Expected:** All should succeed

---

## Copilot Instructions Preview

This repository includes **`.github/instructions/`** with context-aware instruction files

**What it does:**
- Automatically applied to all Copilot interactions
- Encodes Clean Architecture rules
- Enforces DDD patterns
- Specifies .NET 10 conventions

**No setup needed** - It just works!

---

## Ready to Begin

**Next Module:** [Copilot Features Tour](01-copilot-features-tour.md)

Use the **Marp preview** to navigate between modules
