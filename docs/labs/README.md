# Workshop Lab Walkthroughs

This directory contains detailed, step-by-step walkthroughs for all workshop labs.

## 📚 Lab Structure

Each lab is designed as a standalone guide with:

- **Clear learning objectives**
- **Step-by-step instructions**
- **Expected code outputs**
- **Troubleshooting guidance**
- **Extension exercises**
- **Success criteria**


## 🎥 Workshop Presentations

Please refer to [presentations](../presentations/README.md) for the full list of workshop presentations.

---

## 🎯 Labs Overview

### [Lab 1: Test-Driven Development](lab-01-tdd.md)

**Duration**: 30 minutes

Learn to follow the Red-Green-Refactor TDD cycle with AI assistance.

**What You'll Build**:

- `INotificationService` interface
- Comprehensive xUnit test suite (RED phase)
- `NotificationService` implementation (GREEN phase)
- Code quality improvements (REFACTOR phase)

**Key Skills**:

- Writing tests before implementation
- Using Copilot Instructions for consistent code quality
- Understanding TDD benefits and common mistakes
- Generating tests with Copilot or Claude Code

**Prerequisites**:

- Repository cloned and personal branch created from `main`
- VS Code with GitHub Copilot or Claude Code enabled
- .NET 10 SDK installed
---

### [Lab 2: From Requirements to Code](lab-02-requirements-to-code.md)

**Duration**: 45 minutes

Transform vague user stories into working, tested features.

**What You'll Build**:

- Priority value object/enum (DDD pattern)
- Task entity with Priority and DueDate
- CreateTaskCommand/Service with handler
- POST /tasks API endpoint with validation
- Full test coverage (unit + integration)

**Key Skills**:

- Decomposing user stories with Copilot or Claude Code
- Generating acceptance criteria
- Implementing features across all layers (Domain → Application → API)
- Maintaining Clean Architecture principles
- Full-stack TDD workflow

**Prerequisites**:

- Completed Lab 1
- Understanding of Red-Green-Refactor cycle

---

### [Lab 3: Code Generation & Refactoring](lab-03-generation-and-refactoring.md)

**Duration**: 45 minutes

Generate complete API endpoints and modernize legacy code.

**What You'll Build**:

- Complete CRUD API (GET, PUT, DELETE endpoints)
- Query handlers or service methods
- Refactored `LegacyTaskProcessor` with modern patterns
- Code following Object Calisthenics principles

**Key Skills**:

- Using Copilot's automatic workspace context for context awareness
- Using `#file` and `#selection` context variables
- Using `/refactor` command for legacy code
- Applying Object Calisthenics (guard clauses, no abbreviations, wrap primitives)
- Multi-file refactoring with Copilot Edits

**Prerequisites**:

- Completed Labs 1 and 2
- Familiar with GitHub Copilot and Claude Code fundamentals

---

### [Lab 4: Testing, Documentation & Workflow](lab-04-testing-documentation-workflow.md)

**Duration**: 15 minutes

Complete the development lifecycle with AI-assisted testing, docs, and PR preparation.

**What You'll Build**:

- Comprehensive test suites using `/tests`
- XML documentation using `/doc`
- API documentation in README
- Conventional Commit messages
- Complete PR description with checklist

**Key Skills**:

- Generating test coverage with `/tests` command
- Creating documentation with `/doc` command
- Writing Conventional Commits
- Using full workspace context for PR descriptions
- Preparing code for review

**Prerequisites**:

- Completed Labs 1, 2, and 3
- Git initialized with commits

---

## Part 2: [Customizing your Agentic Engineering Workflow](../presentations/modules/part2/00-welcome-recap.md)

### [Lab 06: Skills & Customization Hierarchy](lab-06-skills-and-customization.md)

**Duration**: 25-30 minutes

Understand the complete customization hierarchy and the new Skills system.

**What You'll Learn**:

- Four types of customization (Prompts, Instructions, Skills, Agents)
- When to use each customization type
- How Skills differ from Agents
- Hands-on exploration of the test-data-generator skill
- Decision-making framework for choosing the right approach

**What You'll Explore**:

- `#test-data-generator` skill (domain knowledge without tool access)
- Comparing skills vs agents with practical scenarios
- Decision exercises: which customization type to use?

**Key Skills**:

- Understanding the customization hierarchy
- Invoking skills with `#skill-name`
- Distinguishing between knowledge (skills) and workflows (agents)
- Making informed customization choices

**Prerequisites**:

- Completed Part 1 (Labs 1-4) or familiar with basic GitHub Copilot or Claude Code usage
- Access to workshop repository with skills configured

---

### [Lab 07: Introduction to Custom Copilot Agents](lab-07-custom-agents-intro.md)

**Duration**: 30 minutes

Learn about custom agents and how they differ from standard GitHub Copilot and Claude Code interactions.

**What You'll Explore**:

- Mental models: Agents vs Instructions vs Prompts
- Three workshop agents: Architecture Reviewer, Backlog Generator, Test Strategist
- Hands-on practice with each agent
- Understanding agent capabilities and limitations

**Key Skills**:

- Selecting appropriate agents for tasks
- Invoking agents via dropdown selector
- Interpreting structured agent outputs
- Understanding agent design patterns

**Prerequisites**:

- Completed Lab 06: Skills & Customization
- **Recommended**: familiarity with the customization hierarchy from Lab 06

---

### [Lab 08: Workflow Agents in Action](lab-08-workflow-agents.md)

**Duration**: 30 minutes

Apply custom agents in real development workflows.

**What You'll Build**:

- User stories for notification feature (with Backlog Generator)
- Architecture review of Task aggregate (with Architecture Reviewer)
- Test strategy for TaskService (with Test Strategist)

**Key Skills**:

- Integrating agents into development workflow
- Comparing standard chat vs custom agents
- Sequential agent usage patterns
- Iterating on agent outputs

**Prerequisites**:

- Completed Lab 07
- Access to custom agents in repository

---

### [Lab 09: Agent Design Principles](lab-09-agent-design.md)

**Duration**: 25 minutes

Learn how custom agents are designed and structured.

**What You'll Learn**:

- Seven key agent components
- Agent instruction patterns
- Testing and iteration strategies
- Common design pitfalls

**Key Skills**:

- Analyzing agent definitions
- Understanding agent architecture
- Identifying quality patterns
- Recognizing anti-patterns

**Prerequisites**:

- Completed Lab 08
- Familiarity with all three workshop agents

---

### [Lab 10: Build Your Own Agent (Capstone)](lab-10-capstone-build-agent.md)

**Duration**: 45 minutes

Design, build, test, and document your own custom agent.

**What You'll Build**:

- A custom agent for your chosen role
- Test scenarios validating agent behavior
- Documentation for team usage
- Iteration plan for improvements

**Key Skills**:

- End-to-end agent development
- Writing effective agent instructions
- Testing with real scenarios
- Documenting agent usage

**Prerequisites**:

- Completed Labs 06-09
- Understanding of agent design guide

---

## 🚀 Getting Started

### First Time Setup

1. **Clone the repository**:

   ```bash
   git clone https://github.com/mcollier/ai-dev-workshop.git
   cd ai-dev-workshop
   ```

2. **Create your own branch from `main`**:

   ```bash
   git checkout main
   git pull
   git checkout -b my-workshop-branch
   ```
   _Replace `my-workshop-branch` with your name or a unique identifier._

3. **Open in VS Code**:

   ```bash
   code .
   ```

4. **Verify environment**:

   ```bash
   dotnet --version  # Should be 10.0 or higher
   dotnet build      # Should succeed
   dotnet test       # Should pass
   ```

5. **Verify Copilot**:
   - GitHub Copilot extension or CLI installed
   - Signed in to GitHub
   - Instructions automatically load from `.github/instructions/` based on file context

6. **Verify Claude Code**:
   - Claude Code CLI and/or VS Code extension installed
   - Signed into Claude Code account
   - Skills load from `.claude/skills` via `/skills` command
---

## 📖 How to Use These Walkthroughs

### For Participants

**Follow Along Mode**:

- Read each section before typing
- Copy prompts exactly as shown
- Compare your results with expected outputs
- Complete extension exercises if time permits

**Self-Paced Mode**:

- Work through labs at your own pace
- Take breaks between labs
- Commit your work after each lab
- Reference troubleshooting sections as needed

**Review Mode**:

- Use as reference during workshop
- Jump to specific sections as needed
- Check expected outputs when stuck

---

## 🎓 Learning Path

### Suggested Progression

#### Part 1: Fundamentals

```text
Lab 1 (TDD Basics)
    ↓
Lab 2 (Full-Stack Feature)
    ↓
Lab 3 (Generation & Refactoring)
    ↓
Lab 4 (Documentation & Workflow)
```

#### Part 2: Customizing your Agentic Engineering Workflow

```text
Lab 6 (Skills & Customization)
    ↓
Lab 7 (Custom Agents Intro)
    ↓
Lab 8 (Workflow Agents)
    ↓
Lab 9 (Agent Design)
    ↓
Lab 10 (Build Your Own Agent)
    ↓
Apply to Real Projects! 🎉
```

---

## 🛠️ Workshop Technology Stack

### Core Technologies

- **.NET 10** - Modern C# with latest features
- **xUnit v3** - Testing framework
- **FakeItEasy** - Mocking library
- **Minimal APIs** - Lightweight web API pattern

### Architecture Patterns

- **Clean Architecture** - Domain/Application/Infrastructure/API layers
- **DDD (Domain-Driven Design)** - Aggregates, value objects, repositories
- **CQRS** - Separate commands and queries
- **TDD** - Test-Driven Development

### Development Tools

- **VS Code** - Primary editor
- **GitHub Copilot** and/or **Claude Code** - AI pair programmer (labs cover both)
- **Git** - Version control

### Coding Conventions

Automatically enforced context-aware guidance:

- **GitHub Copilot:** `.github/copilot-instructions.md` (repo-wide) and `.github/instructions/*.instructions.md` (path-scoped)
- **Claude Code:** `CLAUDE.md` (repo root)

Both encode the same standards:

- File-scoped namespaces
- Sealed classes by default
- Guard clauses (no else)
- Async/await throughout
- Structured logging with ILogger
- Conventional Commits

---

## 📋 Pre-Workshop Checklist

### System Requirements

- [ ] **OS**: Windows 10+, macOS 10.15+, or Linux
- [ ] **.NET 10 SDK**: `dotnet --version` shows 9.0+
- [ ] **VS Code**: Latest stable version
- [ ] **Git**: Version 2.30+

### VS Code Extensions

- [ ] **GitHub Copilot** (GitHub.copilot)
- [ ] **C# Dev Kit** (ms-dotnettools.csdevkit)

### GitHub Copilot

- [ ] Active subscription (Individual, Business, or Enterprise)
- [ ] Signed in to GitHub in VS Code
- [ ] Copilot enabled (check status bar)
- [ ] Tested inline suggestions (try typing a comment)

### Claude Code
- [ ] Active subscription (Pro, Max, Team, or Enterprise)
- [ ] Signed into Claude Code CLI

### Repository

- [ ] Repository cloned locally
- [ ] Personal branch created from `main`
- [ ] `dotnet build` succeeds
- [ ] `dotnet test` passes
- [ ] `.github/instructions/` directory exists with instruction files

---

## 🐛 Common Issues & Solutions

### "Copilot not suggesting anything"

**Symptoms**: No gray text completions appear  
**Solutions**:

1. Check Copilot status bar icon (should not show error)
2. Sign out and back in to GitHub
3. Restart VS Code
4. Check subscription status at [https://github.com/settings/copilot](https://github.com/settings/copilot)

### "Build fails with SDK errors"

**Symptoms**: `dotnet build` shows SDK not found  
**Solutions**:

1. Install .NET 10 SDK from dotnet.microsoft.com
2. Restart terminal/VS Code after installation
3. Verify: `dotnet --version`
4. Check PATH environment variable

### "Tests not found"

**Symptoms**: `dotnet test` shows "No tests found"  
**Solutions**:

1. Ensure you're in repository root
2. Verify test projects reference xUnit: `dotnet list package`
3. Rebuild solution: `dotnet build`
4. Check test project has `<IsPackable>false</IsPackable>`

### "Copilot Instructions not working"

**Symptoms**: Code doesn't follow conventions  
**Solutions**:

1. Verify `.github/instructions/` directory exists with instruction files
2. Check instruction files have correct `applyTo:` frontmatter in YAML
3. Reload VS Code window: `F1` → "Developer: Reload Window"
4. Be explicit in prompts: "Follow .NET conventions"

---

## 📚 Additional Resources

### Documentation

- [Main Workshop README](../../README.md) - Workshop overview

### External Links

- [GitHub Copilot Docs](https://docs.github.com/en/copilot)
- [Claude Code Docs](https://code.claude.com/docs/en/overview)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [xUnit Documentation](https://xunit.net/)
- [.NET Architecture Guides](https://dotnet.microsoft.com/learn/dotnet/architecture-guides)

### GitHub Copilot Features

- [Copilot Chat](https://docs.github.com/en/copilot/using-github-copilot/asking-github-copilot-questions-in-your-ide)
- [Slash Commands](https://docs.github.com/en/copilot/using-github-copilot/getting-code-suggestions-in-your-ide-with-github-copilot#using-code-snippets-in-chat)
- [Context Variables](https://docs.github.com/en/copilot/using-github-copilot/asking-github-copilot-questions-in-your-ide#using-symbols)
- [Copilot Instructions](https://docs.github.com/en/copilot/customizing-copilot/adding-custom-instructions-for-github-copilot)

### Claude Code Features
- [How Claude Code Works](https://code.claude.com/docs/en/how-claude-code-works)
- [Use Claude Code - Instructions and Memories](https://code.claude.com/docs/en/memory)
- [Platforms and Integrations](https://code.claude.com/docs/en/platforms)

---

## 🎯 Workshop Goals Recap

### Part 1: Fundamentals

By completing Part 1 labs, you will:

✅ **Master TDD with AI** - Write tests first, implement second  
✅ **Understand Clean Architecture** - Maintain proper layer separation  
✅ **Apply DDD Patterns** - Use aggregates, value objects, repositories  
✅ **Generate Quality Code** - Leverage Copilot Instructions for consistency  
✅ **Refactor Effectively** - Modernize legacy code with AI assistance  
✅ **Document Thoroughly** - Generate comprehensive documentation quickly  
✅ **Follow Best Practices** - Conventional commits, proper testing, code review preparation

### Part 2: Customizing your Agentic Engineering Workflow

By completing Part 2 labs, you will:

✅ **Master Interaction Models** - Know when to use Ask, Plan, or Agent modes  
✅ **Leverage Custom Agents** - Use specialized agents for architecture, testing, and planning  
✅ **Design Effective Agents** - Understand agent structure and best practices  
✅ **Build Custom Agents** - Create and test your own agents for team workflows  
✅ **Integrate into Workflows** - Apply agents throughout development lifecycle  
✅ **Establish Governance** - Manage agent library with versioning and review processes

---

**Ready to start?**

→ **Part 1**: [Begin with Lab 1: TDD](lab-01-tdd.md)  
→ **Part 2**: [Begin with Lab 06: Skills & Customization](lab-06-skills-and-customization.md)
