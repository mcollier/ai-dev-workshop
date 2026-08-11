# Story 8: Sort Tasks by Due Date

**As a** task manager user
**I want** to sort my task list by due date
**So that** I can see what's due soon and plan my time accordingly

## Acceptance Criteria

- [ ] `GET /tasks?sortBy=dueDate` sorts ascending; tasks without a due date appear last
- [ ] `sortOrder=desc` reverses the order (nulls still last)
- [ ] No parameter returns tasks in the unchanged default order

**Dependencies:** Stories 4, 5, 6
**Estimate:** 3 points
**Priority:** Medium
