# Story 3: Create and Update Tasks with Priority via API

**As a** task manager user
**I want** to set and change priority through the API
**So that** I can organize my work by importance

## Acceptance Criteria

- [ ] `POST /tasks` accepts optional `priority` (defaults to `Medium`), returns 201 with it included
- [ ] `PUT /tasks/{id}/priority` updates priority, returns 200; 404 if not found; 400 if invalid value
- [ ] `GET /tasks/{id}` response includes `priority`
- [ ] API integration tests cover all of the above

**Dependencies:** Stories 1, 2
**Estimate:** 5 points
**Priority:** High
