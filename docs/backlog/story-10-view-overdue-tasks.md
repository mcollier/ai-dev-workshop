# Story 10: View Overdue Tasks

**As a** task manager user
**I want** to see which tasks are overdue
**So that** I can catch up on missed deadlines and reprioritize my work

## Acceptance Criteria

- [ ] `GET /tasks?overdue=true` returns only tasks with a due date in the past (tasks without a due date are excluded)
- [ ] `GET /tasks?overdue=false` returns only tasks not overdue (future due date or null)
- [ ] New repository method uses business-intent naming (e.g. `FindOverdueTasksAsync`)

**Dependencies:** Stories 4, 5, 6
**Estimate:** 3 points
**Priority:** Medium
