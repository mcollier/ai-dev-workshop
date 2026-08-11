# Story 4: Add Due Date to Task Domain Model

**As a** task manager user
**I want** optional due dates on tasks
**So that** I can track deadlines

## Acceptance Criteria

- [ ] `Task` has a nullable `DueDate` (`DateOnly?`)
- [ ] `Task.Create` accepts an optional due date; `SetDueDate(DateOnly?)` business method (`null` clears it)
- [ ] Domain invariant: due date must be in the future when provided, otherwise throws (e.g. `ArgumentException`)
- [ ] Domain unit tests: past date rejected, future date accepted, null accepted, `UpdatedAt` bumped

**Dependencies:** None (can be developed in parallel with Stories 1-3)
**Estimate:** 3 points
**Priority:** High
