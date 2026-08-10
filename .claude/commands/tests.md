---
description: Generate xUnit tests for a method, class, or file, following this repo's TDD and testing conventions.
argument-hint: [file path, class name, or paste code]
---

Generate xUnit v3 tests for: $ARGUMENTS

Follow the testing conventions in `CLAUDE.md`:

- Use FakeItEasy for any collaborator interfaces; use Testcontainers only if
  the code under test is Infrastructure/Api (integration-level).
- Put unit tests for Domain/Application behavior under
  `src/TaskManager.UnitTests/`; put integration tests for
  Infrastructure/Api wiring under `src/TaskManager.IntegrationTests/`.
- Name tests descriptively: `Method_UnderTest_ExpectedBehavior`.
- Cover the happy path, at least one edge case, and at least one
  error/validation path — don't stop at the happy path alone.
- If the target type is a DDD aggregate/entity, construct it only through
  its public factory methods (e.g. `Task.Create`), never by bypassing
  invariants. Reuse `.claude/skills/test-data-generator` if you need several
  varied instances rather than one example.
- After generating the tests, note which ones are expected to fail against
  the current implementation (RED phase) versus which should already pass.
