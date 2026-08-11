# Lab 3: Code Generation & Refactoring

**Duration**: 45 minutes  
**Tools**: GitHub Copilot or Claude Code (steps are shown side by side below)  
**Learning Objectives**:

- Generate complete API endpoints using your AI coding tool's file-context references
- Refactor legacy code using slash/custom commands and inline or REPL-based prompts
- Apply Object Calisthenics principles with AI assistance
- Use your AI coding tool's automatic workspace context to understand and modify existing code
- Leverage multi-file editing (Copilot Edits or Claude Code) for cross-cutting refactoring

---

## 🎥 Demo: Plan First with Agents (Custom Agents Demo)

See [Lab 03 Demo: Plan First with Agents](lab-03-demo-plan-first-with-agents.md).
The facilitator models asking Copilot Agent Mode for a plan before making
major changes, then using `@planner` and `@engineer` to refine and execute it.

---

## Overview

In this lab, you'll work with both new and existing code:

- **Part 1**: Generate new API endpoints efficiently using your AI coding tool's context awareness
- **Part 2**: Refactor legacy code (`LegacyTaskProcessor`) to modern standards
- **Part 3**: Apply advanced refactoring patterns (Object Calisthenics)
- **Part 4**: Apply a cross-cutting rename across multiple files (Copilot Edits or Claude Code)


---

## 🚀 Agent Mode Challenge: Go Beyond Ask/Plan

For this lab, try using your AI coding tool's autonomous agent mode for at least one major task (such as refactoring `LegacyTaskProcessor` or generating all CRUD endpoints at once). Agent mode plans and executes multi-step, multi-file changes, and can invoke additional tools automatically.

**How to try it:**

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot                                                      | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| --------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Switch Copilot Chat to "Agent" mode (dropdown in chat panel)                                                                                  | Run `claude` in the integrated terminal — the CLI operates agentically by default |
| Describe your goal in natural language (e.g., "Refactor LegacyTaskProcessor to use async/await, add logging, and follow Object Calisthenics") | Enter the same goal as a prompt in the REPL                                       |
| Review the plan and results, iterate as needed                                                                                                | Review the plan and results, iterate as needed                                    |
| For advanced users: reference MCP tools directly (e.g., "Evaluate my API endpoints using aitk-evaluation_planner")                            | For advanced users: reference any configured MCP tools directly in your prompt    |

**Compare:**
- What did agent mode do differently than a single-shot Ask/Plan prompt?
- Did it propose a plan, use multiple tools, or make changes across files?
- Was the result more complete or did it need more review?

**Facilitator Tip:**
Encourage participants to share their agent mode results (from either tool) and discuss when this approach is most effective.

---

## Prerequisites

- ✅ Completed Lab 1 (TDD) and Lab 2 (Requirements to Code)
- ✅ Familiar with your AI coding tool's chat interface (Copilot Chat or the Claude Code REPL), inline/ad-hoc prompts, and slash or custom commands
- ✅ Understanding of Clean Architecture layers
- ✅ Repository at clean state

---

## Part 1: Generate API Endpoints (20 minutes)

### Scenario: Complete CRUD Operations

You have the POST /tasks endpoint from Lab 2. Now complete the REST API with GET, PUT, and DELETE operations.

### 1.1 Understand Existing Structure

Before generating new code, understand what exists — both tools automatically search your workspace, so just ask directly:

```text
Show me the API endpoint structure. Where are endpoints defined and how are they organized?
```

Your AI coding tool should identify:

- `src/TaskManager.Api/Extensions/EndpointExtensions.cs` - Endpoint definitions
- Minimal API pattern with extension methods
- Existing POST /tasks endpoint
- DI container registration in `Program.cs`

### 1.2 Generate Query: GET /tasks (List All)

#### Step 1: Design Query Handler

Ask your AI coding tool:

```text
Create a GetTasksQuery handler in the Application layer following CQRS pattern. 
It should:
- Return all tasks from ITaskRepository
- Support optional filtering by TaskStatus (enum: Todo, InProgress, Done)
- Order results by CreatedAt descending
Include unit tests using xUnit and FakeItEasy
```

**Expected Output**:

- `src/TaskManager.Application/Queries/GetTasksQuery.cs`
- `src/TaskManager.Application/Queries/GetTasksQueryHandler.cs`
- `tests/TaskManager.UnitTests/Application/Queries/GetTasksQueryHandlerTests.cs`

**Note**: The domain model uses `TaskStatus` enum (Todo/InProgress/Done) rather than a boolean `IsCompleted` field.

#### Step 2: Implement Endpoint

Reference the existing endpoint file explicitly so the new code matches its style:

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot       | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Use the `#file` context variable: `#file:src/TaskManager.Api/Extensions/EndpointExtensions.cs` | Use an `@` file mention: `@src/TaskManager.Api/Extensions/EndpointExtensions.cs`  |

```text
Add a GET /tasks endpoint in <file-reference> that:
- Accepts optional query parameter: status (string: "Todo", "InProgress", or "Done")
- Calls GetTasksQueryHandler
- Returns 200 OK with array of TaskResponse
- Uses async/await and proper error handling
Follow the existing endpoint pattern
```

Replace `<file-reference>` with `#file:src/TaskManager.Api/Extensions/EndpointExtensions.cs` (Copilot) or `@src/TaskManager.Api/Extensions/EndpointExtensions.cs` (Claude Code).

**Expected Addition**:

```csharp
public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
{
    // ... existing POST /tasks endpoint ...

    // GET /tasks
    app.MapGet("/tasks", async (
        [FromQuery] string? status,
        GetTasksQueryHandler handler,
        CancellationToken cancellationToken) =>
    {
        try
        {
            // Parse status string to TaskStatus enum if provided
            TaskStatus? taskStatus = null;
            if (!string.IsNullOrEmpty(status) && 
                Enum.TryParse<TaskStatus>(status, true, out var parsed))
            {
                taskStatus = parsed;
            }
            
            var query = new GetTasksQuery { Status = taskStatus };
            var tasks = await handler.HandleAsync(query, cancellationToken);
            var response = tasks.Select(t => new TaskResponse
            {
                Id = t.Id.Value,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority.ToString(),
                Status = t.Status.ToString(),
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt
            });
            
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem(
                detail: ex.Message,
                statusCode: 500);
        }
    })
    .WithName("GetTasks")
    .WithTags("Tasks")
    .Produces<IEnumerable<TaskResponse>>(200)
    .Produces<ProblemDetails>(500);
}
```

### 1.3 Generate Query: GET /tasks/{id} (Get by ID)

Ask your AI coding tool:

```text
Create a GetTaskByIdQuery handler in Application layer that:
- Accepts a Guid taskId
- Returns single task from repository or null
- Throws ArgumentException if taskId is empty
Include unit tests with FakeItEasy

Then add GET /tasks/{id} endpoint that returns 200 OK or 404 Not Found
```

**Key Learning**: Notice how your AI coding tool reuses patterns from existing code (error handling, response mapping, validation).

### 1.4 Generate Command: PUT /tasks/{id} (Update)

Make an inline, in-context edit request instead of a full chat conversation:

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot                      | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                                                                                                  |
| ------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Open `EndpointExtensions.cs`, position cursor after the GET endpoints, press `Ctrl+I` / `Cmd+I` (Inline Chat) | Open `EndpointExtensions.cs` in your editor, then in the Claude Code REPL reference the file and cursor location, e.g. `In EndpointExtensions.cs, after the GET endpoints, add...` |

Enter:

```text
Add PUT /tasks/{id} endpoint that:
- Accepts UpdateTaskRequest (title, description, priority, dueDate)
- Creates UpdateTaskCommand
- Calls UpdateTaskCommandHandler
- Returns 200 OK with updated task or 404 if not found
Include command handler in Application layer with tests
```

### 1.5 Generate Command: DELETE /tasks/{id}

Ask your AI coding tool:

```text
Create DeleteTaskCommand and handler that:
- Accepts taskId
- Removes task from repository
- Returns success (void)
- Throws if task not found

Add DELETE /tasks/{id} endpoint returning 204 No Content or 404 Not Found
Include unit tests for handler
```

### 1.6 Run and Test

```bash
dotnet build
dotnet test
cd src/TaskManager.Api
dotnet run
```

Test the full API:

```bash
# Create a task
curl -X POST http://localhost:5215/tasks \
  -H "Content-Type: application/json" \
  -d '{"title": "Test Task", "priority": "Medium", "dueDate": "2025-10-30T12:00:00Z"}'

# List all tasks
curl http://localhost:5215/tasks

# Get specific task (use ID from create response)
curl http://localhost:5215/tasks/{id}

# Update task
curl -X PUT http://localhost:5215/tasks/{id} \
  -H "Content-Type: application/json" \
  -d '{"title": "Updated Task", "priority": "High", "dueDate": "2025-11-01T12:00:00Z"}'

# Delete task
curl -X DELETE http://localhost:5215/tasks/{id}
```

---

## Part 2: Refactor Legacy Code (15 minutes)

### Scenario: Legacy Task Processor

The repository contains `LegacyTaskProcessor.ProcessTask` - poorly written code that needs refactoring.

### 2.1 Find the Legacy Code

Ask your AI coding tool directly (both automatically search your workspace):

```text
Find the LegacyTaskProcessor class
```

**Location**: `src/TaskManager.Infrastructure/Legacy/LegacyTaskProcessor.cs`

### 2.2 Analyze Current Issues

Analyze the problematic method before changing it:

1. Navigate to the `ProcessTask` method
2. Select the entire method

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                              |
| ---------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| Use Inline Chat (`Ctrl+I` or `Cmd+I`): `/explain`                                        | Reference the method in the REPL and ask: `Explain what this method does and identify any code quality issues` |

Your AI coding tool should identify issues:

- ❌ Nested if statements (6+ indentation levels)
- ❌ Synchronous blocking code (`Thread.Sleep`)
- ❌ Poor error handling (exceptions swallowed with empty catch)
- ❌ No logging
- ❌ Magic numbers (1, 2, 50) and strings
- ❌ Long method (80+ lines with multiple responsibilities)
- ❌ Poor naming (`data`, `flag`, `type`, `i`)
- ❌ String concatenation in loops (inefficient)
- ❌ Mixed concerns (file I/O in processing logic)
- ❌ Not following guard clause pattern

### 2.3 Refactor the Method

Select the entire `ProcessTask` method:

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Use the `/refactor` slash command in Copilot Chat                                        | Ask directly in the REPL (no `/refactor` command defined yet — see `todo.md`)     |

```text
Refactor this method to follow Clean Code principles:
1. Use guard clauses (fail fast, no nested ifs)
2. Convert to async/await
3. Add structured logging with ILogger<LegacyTaskProcessor>
4. Extract smaller methods for single responsibilities
5. Use proper exception handling (don't swallow exceptions)
6. Replace magic numbers with constants or enums
7. Use meaningful parameter and variable names
8. Use StringBuilder for string operations in loops
9. Separate concerns: extract file I/O to an interface (ITaskOutputWriter)
10. Follow Object Calisthenics: max 2 levels of indentation per method
Follow our .NET coding conventions and make the class sealed
```

**Expected Improvements**:

- Strongly-typed `ProcessingType` enum instead of `int type`
- Guard clauses for null/empty input (fail fast)
- Private helper methods: `ProcessFormatting()`, `ProcessCapitalization()`, `TruncateIfNeeded()`
- Async signature: `Task<string> ProcessTaskAsync(...)`
- Constructor injection: `ILogger<LegacyTaskProcessor>`, `ITaskOutputWriter?`
- Proper error handling with logging
- `StringBuilder` for efficient string building
- Meaningful names: `taskIdentifier`, `inputText`, `processingType`, `shouldInvertCase`

**Expected Refactored Code**:

```csharp
public async Task<ProcessingResult> ProcessTaskBatchAsync(
    IEnumerable<TaskItem> tasks,
    CancellationToken cancellationToken = default)
{
    if (tasks == null)
        throw new ArgumentNullException(nameof(tasks));

    _logger.LogInformation("Starting batch processing of tasks");

    var taskList = tasks.ToList();
    if (taskList.Count == 0)
    {
        _logger.LogInformation("No tasks to process");
        return ProcessingResult.Empty;
    }

    var result = new ProcessingResult();

    foreach (var task in taskList)
    {
        await ProcessSingleTaskAsync(task, result, cancellationToken);
    }

    _logger.LogInformation(
        "Batch processing completed: {SuccessCount} succeeded, {FailureCount} failed",
        result.SuccessCount,
        result.FailureCount);

    return result;
}

private async Task ProcessSingleTaskAsync(
    TaskItem task,
    ProcessingResult result,
    CancellationToken cancellationToken)
{
    if (!IsTaskValid(task))
    {
        _logger.LogWarning("Invalid task {TaskId} skipped", task.Id);
        result.AddFailure(task.Id, "Invalid task data");
        return;
    }

    try
    {
        await ExecuteTaskProcessingAsync(task, cancellationToken);
        result.AddSuccess(task.Id);
        
        _logger.LogInformation(
            "Task {TaskId} processed successfully",
            task.Id);
    }
    catch (Exception ex)
    {
        _logger.LogError(
            ex,
            "Failed to process task {TaskId}",
            task.Id);
        result.AddFailure(task.Id, ex.Message);
    }
}

private static bool IsTaskValid(TaskItem task)
{
    if (task == null) return false;
    if (string.IsNullOrWhiteSpace(task.Title)) return false;
    if (task.Priority < 0 || task.Priority > 3) return false;
    
    return true;
}

private async Task ExecuteTaskProcessingAsync(
    TaskItem task,
    CancellationToken cancellationToken)
{
    // Update task status
    task.Status = TaskStatus.Processing;
    await _repository.UpdateAsync(task, cancellationToken);

    // Simulate processing
    await Task.Delay(100, cancellationToken);

    // Complete task
    task.Status = TaskStatus.Completed;
    task.CompletedAt = DateTime.UtcNow;
    await _repository.UpdateAsync(task, cancellationToken);
}
```

### 2.4 Generate Tests for Refactored Code

Select the refactored method:

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                                     |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------- |
| Use the `/tests` custom command                                                          | Use the `/tests` custom command, or ask directly in the REPL: `Generate xUnit tests for this method using FakeItEasy` |

Verify generated tests cover:

- ✅ Null input throws ArgumentNullException
- ✅ Empty collection returns empty result
- ✅ Valid tasks are processed successfully
- ✅ Invalid tasks are logged and skipped
- ✅ Processing exceptions are caught and logged
- ✅ Result contains correct success/failure counts

Run tests:

```bash
dotnet test
```

---

## Part 3: Apply Object Calisthenics (10 minutes)

### Scenario: Further Code Quality Improvements

Apply Object Calisthenics rules from our .NET instructions (Section 7).

### 3.1 Review Object Calisthenics Rules

Ask your AI coding tool:

```text
What are the Object Calisthenics rules from our .NET coding instructions?
```

Key rules:

1. Only one level of indentation per method
2. Don't use 'else' keyword (guard clauses)
3. Wrap all primitives and strings
4. First-class collections
5. One dot per line (avoid call chains)
6. Don't abbreviate names
7. Keep all entities small
8. No classes with more than two instance variables
9. No getters/setters/properties (for domain entities)

### 3.2 Apply: Wrap Primitives

Find places where primitive types are used directly for domain concepts.

Ask your AI coding tool:

```text
Review the TaskItem class. Are there primitive types that should be wrapped in value objects following DDD patterns? For example, should task status be an enum or value object instead of a string?
```

**Before**:

```csharp
public class TaskItem
{
    public Guid Id { get; set; }
    public string Status { get; set; } // Primitive obsession
    public int Priority { get; set; } // Magic numbers
}
```

**After** (with AI assistance):

```csharp
public sealed class TaskItem
{
    public TaskId Id { get; private set; }
    public TaskStatus Status { get; private set; }
    public Priority Priority { get; private set; }
}
```

### 3.3 Apply: First-Class Collections

Find collections that are exposed directly and wrap them.

Ask your AI coding tool:

```text
If we have a class with a List<Task> property, how should we wrap it following Object Calisthenics and DDD patterns?
```

**Before**:

```csharp
public class TaskList
{
    public List<TaskItem> Tasks { get; set; }
}
```

**After**:

```csharp
public sealed class TaskCollection
{
    private readonly List<TaskItem> _tasks;

    public TaskCollection(IEnumerable<TaskItem> tasks)
    {
        _tasks = tasks?.ToList() ?? new List<TaskItem>();
    }

    public int Count => _tasks.Count;
    
    public IReadOnlyList<TaskItem> Items => _tasks.AsReadOnly();

    public void Add(TaskItem task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));
            
        _tasks.Add(task);
    }

    public TaskItem? FindById(TaskId id) => 
        _tasks.FirstOrDefault(t => t.Id == id);
}
```

### 3.4 Apply: No Abbreviations

Expand abbreviated names using a scoped, in-context request:

1. Find abbreviated variable names (e.g., `var t`, `var res`, `int cnt`)
2. Select the code

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                          |
| ---------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------- |
| Inline Chat: "Expand all abbreviated variable names to be fully descriptive"             | Paste the selection into the REPL and ask: "Expand all abbreviated variable names to be fully descriptive" |

**Before**:

```csharp
var res = await _repo.GetAsync(id);
if (res != null)
{
    var cnt = res.Items.Count();
    // ...
}
```

**After**:

```csharp
var result = await _repository.GetAsync(id);
if (result != null)
{
    var itemCount = result.Items.Count();
    // ...
}
```

---

## Part 4: Multi-File Refactoring (Optional, if time)

### Scenario: Rename Across Multiple Files

Use multi-file editing for cross-cutting changes.

### 4.1 Open Multi-File Editing

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot                                                         | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code                                              |
| ------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------ |
| Open Command Palette (`Ctrl+Shift+P` / `Cmd+Shift+P`), search for "Copilot Edits: Open", or use the dedicated Copilot Edits panel in the sidebar | Run `claude` in the integrated terminal — the REPL already operates across multiple files by default, no separate panel needed |

### 4.2 Add Files to Working Set

| <img src="../images/githubcopilot.svg" width="20" alt="GitHub Copilot" /> GitHub Copilot | <img src="../images/claude-color.svg" width="20" alt="Claude Code" /> Claude Code |
| ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Add related files to the Copilot Edits working set                                       | Mention the related files in your prompt with `@` so Claude Code knows the scope  |

Related files for this change:

- `src/TaskManager.Domain/Entities/Task.cs`
- `src/TaskManager.Application/Commands/CreateTaskCommand.cs`
- `src/TaskManager.Application/Commands/CreateTaskCommandHandler.cs`
- `tests/TaskManager.UnitTests/Commands/CreateTaskCommandHandlerTests.cs`

### 4.3 Describe Change

Enter the same request in the Copilot Edits panel or the Claude Code REPL:

```text
Rename the "Title" property to "Name" across all files in the working set. Update:
- Entity property
- Command property
- All references in handlers
- All test assertions
Ensure consistency across the entire codebase
```

### 4.4 Review Proposed Changes

Your AI coding tool will show:

- All files that will be modified
- Exact changes in each file
- Side-by-side diff view

### 4.5 Accept or Reject

- Review each change carefully
- Accept all if changes look correct
- Or accept/reject individual file changes
- Run tests after applying: `dotnet test`

---

## Key Learning Points

### ✅ Context-Aware Code Generation

1. **Automatic workspace context**: Understanding existing structure before generating
2. **`#file` (Copilot) / `@file` (Claude Code)**: Referencing specific files for consistent patterns
3. **Selection-based prompts**: Refactoring specific code sections
4. **Pattern Reuse**: Your AI coding tool learns patterns from existing endpoints

### ✅ Effective Refactoring Workflow

1. **Explain first**: Understand code before changing it (`/explain` in Copilot, a direct prompt in Claude Code)
2. **Refactor with intent**: Automated refactoring with specific goals (`/refactor` in Copilot, a direct prompt in Claude Code)
3. **Generate tests**: Cover refactored code (`/tests` in Copilot or Claude Code)
4. **Iterative**: Refactor in small steps, run tests frequently

### ✅ Code Quality Improvements

1. **Guard Clauses**: Early returns reduce indentation
2. **Async/Await**: Modern patterns for I/O operations
3. **Logging**: Structured logging provides observability
4. **Single Responsibility**: Extracted methods with clear purposes
5. **Object Calisthenics**: Advanced quality constraints

### ✅ Multi-File Editing

1. **Copilot Edits / Claude Code REPL**: Consistent changes across multiple files
2. **Working Set**: Explicitly define scope of changes (Copilot Edits working set, or `@` file mentions in Claude Code)
3. **Review Process**: Always review AI-proposed changes
4. **Safe Refactoring**: Tests validate behavior preservation

---

## Extension Exercises (If Time Permits)

### Exercise 1: Add Pagination

Refactor GET /tasks to support pagination (page, pageSize query parameters).
Use your AI coding tool to:

1. Add pagination to repository
2. Update query handler
3. Modify endpoint
4. Update tests

### Exercise 2: Add Sorting

Add sorting support to GET /tasks (sortBy, sortOrder parameters).
Valid sort fields: title, priority, dueDate, createdAt

### Exercise 3: Extract API Response Builder

Create a dedicated class for building TaskResponse from Task entity.
Use Copilot Edits (or the Claude Code REPL) to update all endpoints to use the builder.

---

## Success Criteria

You've completed this lab successfully when:

- ✅ Full CRUD API endpoints implemented (POST, GET, GET by ID, PUT, DELETE)
- ✅ All endpoints follow consistent patterns
- ✅ LegacyTaskProcessor refactored to modern standards
- ✅ Refactored code follows Object Calisthenics principles
- ✅ Guard clauses used instead of nested ifs
- ✅ Async/await pattern applied throughout
- ✅ Structured logging added
- ✅ All tests passing
- ✅ Code is clean, readable, and maintainable

---

## Troubleshooting

### AI Tool Generates Inconsistent Patterns

**Problem**: New endpoints don't match existing style  
**Solution**: Reference the existing endpoint file explicitly (`#file` in Copilot, `@file` in Claude Code) and state "Follow the existing pattern"

### Refactoring Breaks Tests

**Problem**: Tests fail after refactoring  
**Solution**: This is OK! Update tests to match new behavior. Regenerate tests (`/tests` in Copilot or Claude Code).

### Too Many Changes at Once

**Problem**: Your AI coding tool suggests massive refactoring  
**Solution**: Break into smaller steps. Refactor one method at a time. Run tests after each change.

### Multi-File Edit Misses Files

**Problem**: Copilot Edits (or Claude Code) doesn't update all references  
**Solution**: Use VS Code's built-in "Rename Symbol" (F2) for simple renames. Use Copilot Edits or Claude Code for semantic changes, and always review the diff.

---

## Next Steps

Move on to [**Lab 4: Testing, Documentation & Workflow**](lab-04-testing-documentation-workflow.md) where you'll:

- Generate comprehensive test suites (`/tests` in Copilot or Claude Code)
- Create documentation (`/doc` in Copilot or Claude Code)
- Write Conventional Commit messages
- Draft PR descriptions using full workspace context

---

## Additional Resources

- [Object Calisthenics](https://williamdurand.fr/2013/06/03/object-calisthenics/)
- [Refactoring Techniques](https://refactoring.guru/refactoring/techniques)
- [Clean Code Principles](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles)
- [Minimal APIs in .NET](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
