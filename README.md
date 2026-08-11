# AI Coding Workshop

_The content in this workshop is heavily derived from [https://github.com/centricconsulting/ai-coding-workshop](https://github.com/centricconsulting/ai-coding-workshop). Credit to [Shawn Wallace](https://github.com/shawnewallace) for the inspiration._

This repository contains a hands-on AI development workshop built around a series of presentations and labs. It is designed to help you practice working with GitHub Copilot and Claude Code and related tools in a realistic software delivery flow.

## Workshop contents

- **Presentations:** Modular slide decks for the workshop sessions in [docs/presentations/](docs/presentations/)
- **Guides:** Supporting how-to and reference material in [docs/guides/](docs/guides/)
- **Setup:** Environment preparation checklist in [docs/README.md](docs/README.md)

## Workshop structure

The workshop is organized into multiple sessions that move from fundamentals to more advanced topics:

1. **Part 1: Fundamentals** — setup, instructions, requirements-to-code, generation/refactoring, testing, and documentation
2. **Part 2: Customizing your Agentic Engineering Workflow** — interaction models, skills/customization, custom agents, workflow agents, and agent design

Each section includes presentation material and lab-oriented exercises you can follow along with.

## API Documentation

The Task Manager API (`src/TaskManager.Api`) is a minimal API exposing task CRUD operations. Default base URL when running locally: `http://localhost:5215` (see [TaskManager.Api.http](src/TaskManager.Api/TaskManager.Api.http)).

### Endpoints

#### Health Check

`GET /health` — Returns API readiness status.

#### List Tasks

`GET /tasks` — Returns active tasks by default. Supports optional query parameters:

- `status` (string: `Todo`, `InProgress`, `Done`, `Cancelled`) — filters via the CQRS `GetTasksQuery`, ordered by `createdAt` descending.
- `priority` (comma-separated: `Low`, `Medium`, `High`) — filters active tasks by priority.
- `sortBy=priority` with optional `sortOrder=desc` — sorts results by priority.

**Success Response** (200 OK):

```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Complete project documentation",
    "description": "Write comprehensive API documentation",
    "status": 0,
    "priority": 2,
    "createdAt": "2025-10-20T10:30:00Z",
    "updatedAt": "2025-10-20T10:30:00Z",
    "dueDate": null
  }
]
```

#### Get Task by ID

`GET /tasks/{id}` — Retrieves a single task.

**Success Response** (200 OK): same shape as a single item above.
**Error Responses**: `404 Not Found` if no task exists with the given id.

#### Create Task

`POST /tasks` — Creates a new task.

**Request Body**:

```json
{
  "title": "Complete project documentation",
  "description": "Write comprehensive API documentation",
  "priority": 2
}
```

`priority` is optional (int: `0` Low, `1` Medium, `2` High); defaults to Medium.

**Success Response** (201 Created): the created task, same shape as above.
**Error Responses**: `400 Bad Request` for a missing/empty title or description, or an invalid priority value.

#### Update Task

`PUT /tasks/{id}` — Updates title, description, priority, and due date.

**Request Body**:

```json
{
  "title": "Updated task title",
  "description": "Updated description",
  "priority": 1,
  "dueDate": "2025-11-01"
}
```

`dueDate` is optional (`DateOnly`, ISO 8601 date) and must be in the future when provided.

**Success Response** (200 OK): the updated task.
**Error Responses**: `400 Bad Request` for invalid priority, empty title/description, or a due date that isn't in the future; `404 Not Found` if the task doesn't exist.

#### Update Task Priority

`PUT /tasks/{id}/priority` — Updates only the priority.

**Request Body**:

```json
{ "priority": 2 }
```

**Success Response** (200 OK): the updated task.
**Error Responses**: `400 Bad Request` for an invalid priority; `404 Not Found` if the task doesn't exist.

### Error Response Format

Validation errors are returned as JSON with an `error` field:

```json
{ "error": "Invalid priority value" }
```

## Helpful links

- [Workshop presentations index](docs/presentations/README.md)
- [Pre-workshop checklist](docs/README.md)