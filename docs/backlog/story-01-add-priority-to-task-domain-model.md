# Story 1: Add Priority to Task Domain Model

**As a** task manager user
**I want** tasks to have a priority level
**So that** I can distinguish important tasks from less urgent ones

## Acceptance Criteria

- [ ] `Priority` enum exists with `Low`, `Medium`, `High`
- [ ] `Task` aggregate has a `Priority` property, defaults to `Medium`
- [ ] `Task.Create` factory accepts an optional priority parameter
- [ ] `Task` has an `UpdatePriority(Priority)` business method; updates `UpdatedAt`
- [ ] Domain unit tests cover creation default, explicit priority, and update

**Dependencies:** None
**Estimate:** 2 points
**Priority:** High
