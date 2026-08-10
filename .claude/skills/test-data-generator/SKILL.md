---
name: test-data-generator
description: Generates realistic test data (Task aggregates and related value objects) for TaskManager xUnit tests and Testcontainers-backed integration tests. Use when a test needs multiple valid or edge-case Task instances instead of one hand-written example.
argument-hint: "[entity type] [count]"
user-invocable: true
disable-model-invocation: false
---

# Test Data Generator

This skill generates realistic test data for the TaskManager workshop app's
xUnit unit tests and Testcontainers integration tests. It follows the
Domain rules in `src/TaskManager.Domain/Tasks/` — every generated instance
must be creatable only through the aggregate's public factory methods, never
by reflection or by bypassing invariants.

## When To Use This Skill

- A test needs several `Task` instances with varied, realistic titles,
  descriptions, and statuses (not just one copy-pasted example).
- A test needs boundary/edge-case data: empty strings, max-length titles,
  duplicate titles, tasks in every `TaskStatus` value, etc.
- An integration test seeds a database or in-memory repository with a batch
  of tasks before asserting query/filter behavior.

Don't use this skill for a single, specific example value in a test —
just write that literal inline.

## Usage

Invoke directly: `/test-data-generator Task 10`

Or describe the need in conversation, e.g. "generate 5 tasks in different
statuses for the repository filter tests" — this skill can be
auto-invoked based on its description above.

## Procedure

1. **Identify the entity.** For this repo that's almost always `Task`
   (`src/TaskManager.Domain/Tasks/Task.cs`), created via `Task.Create(title,
   description)` and mutated via `UpdateStatus`/`UpdateDetails`. Never set
   properties directly — there are no public setters, by design.
2. **Pick a realistic value pool**, not `"Test1"`, `"Test2"`, ... Use
   short, plausible task titles (e.g. "Fix login redirect bug", "Write API
   docs for /tasks endpoint") and descriptions of a sentence or two.
3. **Cover the status distribution** requested, cycling through
   `TaskStatus` (`Todo`, `InProgress`, `Done` — check
   `src/TaskManager.Domain/Tasks/TaskStatus.cs` for the current enum
   values) rather than leaving everything at the default.
4. **Include edge cases when the count allows it**: an empty-ish title (if
   validation exists to test), a very long title, a task updated after
   creation (`UpdatedAt != CreatedAt`), and duplicate titles with distinct
   `TaskId`s.
5. **Emit as a C# helper**, not inline duplication: prefer a small
   `TaskTestDataBuilder`/factory method local to the test class or test
   project, matching the pattern in `templates/TaskFactory.cs`.
6. **For integration tests**, emit data as objects ready to insert via the
   repository under test — see `examples/sample-tasks.json` for the shape
   if seeding from a fixture file instead of code.

## Reference Files

- `templates/TaskFactory.cs` — starting point for an in-code test data
  builder using `Task.Create` and `UpdateStatus`.
- `examples/sample-tasks.json` — example fixture shape for
  Testcontainers-backed integration tests that seed from data instead of
  code.

## Constraints

- ALWAYS build instances through `Task.Create` / the aggregate's public
  methods — never via reflection, `FormatterServices`, or object
  initializers that bypass the private constructor.
- ALWAYS vary the data — no two generated tasks should be identical unless
  the test specifically calls for a duplicate-title case.
- NEVER generate secrets, real names, or real email addresses; use
  obviously fictional placeholder values.
