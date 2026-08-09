# CLAUDE.md

This file is Claude Code's repo-wide, always-on instructions file — the
equivalent of `.github/copilot-instructions.md` for GitHub Copilot. Both
files encode the same standards for this repository; keep them in sync when
either changes.

## 1. Repository Purpose

This repository is an AI Dev Workshop. It is designed to be used with GitHub
Copilot or Claude Code, which are AI-powered assistant tools that help
developers write code faster and more efficiently.

- Optimize for workshop clarity and educational value.
- Keep examples practical, explicit, and easy to explain.
- Demonstrate maintainable, test-driven use of GitHub Copilot or Claude Code
  in a real-world development workflow.

## 2. Technology Stack

- **.NET track:** .NET 10, ASP.NET Core Minimal API, xUnit v3, FakeItEasy,
  `ILogger`, OpenTelemetry, and Testcontainers.
- **Docs and workshop content:** Markdown in `docs/`, Marp-compatible slides,
  and VS Code Dev Container workflows.
- Prefer existing repo libraries before introducing new ones.
- Avoid mediator frameworks, heavy abstractions, alternate test frameworks,
  or extra dependencies unless explicitly requested.

## 3. Architecture: Clean Architecture + DDD

Solution layout: `TaskManager.Domain`, `TaskManager.Application`,
`TaskManager.Infrastructure`, `TaskManager.Api`, plus
`TaskManager.UnitTests` and `TaskManager.IntegrationTests`.

Dependencies point inward only:

- **Domain** → no dependencies. Business logic, entities, value objects,
  domain events.
- **Application** → Domain only. Use cases, commands/queries, ports
  (interfaces).
- **Infrastructure** → Application + Domain. Adapters, persistence, external
  integrations.
- **Api** → Infrastructure only. Minimal API endpoints + request/response
  mapping; no business logic.

Prefer feature-oriented folders (e.g. `Tasks/`) over technical groupings.

**Avoid:** circular dependencies, generic CRUD-style domain APIs,
persistence leakage into the domain, domain entities exposed directly as API
contracts, and domain logic inside controllers/endpoints/infrastructure
adapters.

### DDD Modeling Rules

- Model **Aggregates** with factory methods (no public constructors),
  encapsulate invariants, and avoid direct navigation to other aggregates.
- **Entities** live inside aggregates; no public setters; lifecycle managed
  by the root.
- **Value Objects** are immutable with value equality. Prefer
  **strongly-typed IDs** (e.g. `TaskId`) as value objects.
- **Repositories** are interfaces for aggregate roots with
  business-intent method names, not generic CRUD verbs (favor
  `AssignTask`/`MarkComplete` over `Create`/`Update`/`Delete`).

## 4. Coding Style And Naming Conventions

- File-scoped namespaces; one type per file, file name matches type name.
- 4-space indentation.
- `PascalCase` for types/public members, `camelCase` for locals and
  parameters, `ALL_CAPS` for constants.
- Interfaces prefixed with `I` (e.g. `INotificationService`).
- Use `async/await` for all async operations.
- Guard clauses (fail fast) instead of nested `if`/`else`.
- Classes `sealed` by default unless inheritance is intentional.
- Prefer descriptive names over abbreviations; use `nameof` in exceptions
  and guard clauses.

### Object Calisthenics (lightweight, applied when refactoring)

- One level of indentation per method; avoid `else` — use guard clauses.
- Wrap primitives into meaningful types; prefer first-class collections.
- Avoid long call chains ("one dot per line" guideline).
- Don't abbreviate names; keep classes/methods small and focused.
- Limit domain classes' setters; prefer factories and invariants.

## 5. Testing

- **TDD by default**: when asked to implement a feature, propose or write
  tests before code.
- **Framework:** xUnit v3. **Mocking:** FakeItEasy. **Integration:**
  Testcontainers.
- Unit tests target Domain + Application behavior; integration tests target
  Infrastructure + Api wiring.
- Organize tests by feature; name tests descriptively
  (`Method_UnderTest_ExpectedBehavior`).
- After generating code, assume `dotnet build && dotnet test` runs next —
  fix warnings/errors before considering the change done.

## 6. Security And Error Handling

- Validate external input at the boundary and fail fast with specific,
  typed exceptions (e.g. `ArgumentNullException`).
- Avoid catching general `Exception` unless translating it into a clear
  boundary-level outcome or telemetry.
- Never hardcode or log secrets, tokens, credentials, or other sensitive
  data. Use configuration/environment-based settings for infrastructure.
- Keep examples safe by default; avoid insecure shortcuts.

## 7. Documentation Standards

- Keep the root `README.md` focused on overview, setup, and navigation.
- Put detailed docs in `docs/` using the existing folders (`docs/guides/`,
  `docs/labs/`, `docs/presentations/`).
- Use XML comments for public APIs; keep summaries clear and concise.
- Use descriptive file names and working relative links.
- Explain the why, not just the how, in architecture and workshop guidance.
- Keep labs realistic, with prerequisites, expected outcomes, and runnable
  commands, and aligned with the repository's actual code and workflows.

## 8. Conventional Commits

Use `<type>([optional scope]): <description>`, 72-character subject limit.
Types: `feat|fix|docs|style|refactor|perf|test|build|ci|chore|revert`. One
logical change per commit; use scope to denote layer/feature.

```text
feat(api): add order endpoint
fix(domain): correct order validation logic
test(order): add unit tests for order creation
chore: update dependencies
```

## 9. Working Style

- Prefer minimal, focused changes that fit the existing structure.
- Fix root causes instead of layering on workarounds.
- Choose the clearer approach when a clever one hurts workshop readability.
- Do not invent external dependencies without being asked.
- If a rule conflicts, Clean Architecture boundaries win, then DDD, then
  style.

## 10. Related Claude Code Assets

- **Subagents:** `.claude/agents/*.md` — `architecture-reviewer`,
  `backlog-generator`, `test-strategist`, `planner`, `engineer`,
  `modernization`, `quality-gate`, `test-coverage`.
- **Skills:** `.claude/skills/*/SKILL.md` — portable capabilities, same
  format as GitHub Copilot's `.github/skills/`.
- **Commands:** `.claude/commands/*.md` — custom slash commands (e.g.
  `/tests`, `/doc`), the Claude Code equivalent of Copilot's built-in chat
  commands.
