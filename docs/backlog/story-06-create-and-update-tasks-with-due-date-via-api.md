# Story 6: Create and Update Tasks with Due Date via API

**As a** task manager user
**I want** to set and change due dates via the API
**So that** I can track deadlines for my work

## Acceptance Criteria

- [ ] `POST /tasks` accepts optional `dueDate` (ISO 8601); returns 400 if in the past; 201 with it included otherwise
- [ ] `PUT /tasks/{id}/duedate` updates or clears (`null`) the due date, returns 200; 400 if past date; 404 if not found
- [ ] `GET /tasks/{id}` response includes `dueDate` (null if unset)
- [ ] API integration tests cover set, update, clear, and past-date rejection

**Dependencies:** Stories 4, 5
**Estimate:** 5 points
**Priority:** High
