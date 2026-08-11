# ADR 0002: Adopt CQRS for Queries and Commands in the Application Layer

## Status
Accepted

## Context
As the Task Manager API grew beyond simple create/read operations (Lab 3 added filtered/sorted reads, a single-task lookup, and a multi-field update), the `TaskService` class in `TaskManager.Application` was accumulating unrelated responsibilities. Each new read or write operation added another method to the same class, mixing query logic (filtering, ordering) with command logic (validation, mutation, persistence). This made the service harder to extend and to unit test in isolation, and blurred the distinction between operations that only read state and operations that change it.

## Decision
We will use a lightweight CQRS (Command Query Responsibility Segregation) pattern for new read and write use cases in the Application layer:
- Queries live in `TaskManager.Application/Queries/` as a `<Name>Query` record/class paired with a `<Name>QueryHandler` class that depends only on `ITaskRepository`.
- Commands live in `TaskManager.Application/Commands/` as a `<Name>Command` record/class paired with a `<Name>CommandHandler` class that depends only on `ITaskRepository`.
- Each handler exposes a single `HandleAsync` method and is registered in DI (`ServiceExtensions.AddApplicationServices`) as a scoped service, injected directly into the relevant Minimal API endpoint handler.
- This is not a full mediator-based CQRS implementation — there is no in-process bus or pipeline. Handlers are called directly, consistent with the repository-wide guidance to avoid mediator frameworks and unnecessary abstractions.
- Existing `TaskService` methods (`AddTaskAsync`, `UpdatePriorityAsync`, etc.) are left as-is; the pattern applies going forward rather than as a forced migration of already-working code.

## Consequences
- Each new use case gets its own small, focused, independently testable class instead of growing a shared service class.
- Query handlers can evolve independently from command handlers (e.g., adding read-side filtering/sorting without touching write-path validation).
- Endpoint handlers in `EndpointExtensions.cs` stay thin: parse/validate the HTTP request, delegate to a query/command handler, map the result to a `TaskResponse`.
- Slightly more files/boilerplate per use case compared to adding another method to `TaskService`.
- Two patterns now coexist in the Application layer (`TaskService` for the original use cases, query/command handlers for newer ones). This is an accepted, temporary inconsistency; a future ADR may revisit consolidating `TaskService` into the same handler pattern.

## Alternatives Considered
- **Keep growing `TaskService`**: simplest short-term, but continues to mix read/write concerns in one class and makes unit tests less focused.
- **Full mediator library (e.g., MediatR-style)**: adds an abstraction and a package dependency the repository's contribution guidelines explicitly ask to avoid for this workshop; direct handler injection achieves the same separation without it.

---

*See also: [ADR 0001: Use Clean Architecture](0001-use-clean-architecture.md)*
