# Story 7: Filter Tasks by Priority

**As a** task manager user
**I want** to filter my task list by priority level
**So that** I can focus on high-priority items

## Acceptance Criteria

- [ ] `GET /tasks?priority=High` (and multi-value `High,Medium`) filters correctly; no parameter returns all tasks unchanged
- [ ] Returns 400 on invalid priority value
- [ ] New repository method uses business-intent naming (e.g. `FindTasksByPriorityAsync`)

**Dependencies:** Stories 1, 2, 3
**Estimate:** 3 points
**Priority:** Medium
