using TaskManager.Api.Tasks;
using TaskManager.Application.Commands;
using TaskManager.Application.Queries;
using TaskManager.Application.Services;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;
using TaskStatus = TaskManager.Domain.Tasks.TaskStatus;

namespace TaskManager.Api.Extensions;

/// <summary>
/// Extension methods for configuring API endpoints
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    /// Map all Task Manager API endpoints
    /// </summary>
    public static WebApplication MapTaskEndpoints(this WebApplication app)
    {
        // Health check endpoint
        app.MapGet("/health", () => new { Status = "Healthy", Message = "Task Manager API is ready for Lab 3!" })
            .WithName("HealthCheck");

        app.MapGet("/tasks/{id:guid}", GetTaskByIdAsync)
            .WithName("GetTask")
            .WithOpenApi();

        app.MapPost("/tasks", CreateTaskAsync)
            .WithName("CreateTask")
            .WithOpenApi();

        app.MapPut("/tasks/{id:guid}/priority", UpdateTaskPriorityAsync)
            .WithName("UpdateTaskPriority")
            .WithOpenApi();

        app.MapPut("/tasks/{id:guid}/status", UpdateTaskStatusAsync)
            .WithName("UpdateTaskStatus")
            .WithOpenApi();

        app.MapPut("/tasks/{id:guid}", UpdateTaskAsync)
            .WithName("UpdateTask")
            .WithOpenApi();

        app.MapGet("/tasks", GetTasksAsync)
            .WithName("GetTasks")
            .WithOpenApi();

        return app;
    }

    private static async Task<IResult> GetTaskByIdAsync(Guid id, GetTaskByIdQueryHandler getTaskByIdQueryHandler, CancellationToken cancellationToken)
    {
        try
        {
            var task = await getTaskByIdQueryHandler.HandleAsync(new GetTaskByIdQuery { TaskId = id }, cancellationToken);
            return task is null ? Results.NotFound() : Results.Ok(ToResponse(task));
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> CreateTaskAsync(CreateTaskRequest request, TaskService taskService, CancellationToken cancellationToken)
    {
        if (request.Priority is int requestedPriority && !Enum.IsDefined(typeof(Priority), requestedPriority))
            return Results.BadRequest(new { error = "Invalid priority value" });

        var priority = request.Priority is int p ? (Priority)p : Priority.Medium;

        try
        {
            var taskId = await taskService.AddTaskAsync(request.Title, request.Description, priority, cancellationToken);
            var task = await taskService.GetTaskAsync(taskId, cancellationToken);
            return Results.Created($"/tasks/{taskId.Value}", ToResponse(task!));
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> UpdateTaskPriorityAsync(Guid id, UpdatePriorityRequest request, TaskService taskService, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(Priority), request.Priority))
            return Results.BadRequest(new { error = "Invalid priority value" });

        var taskId = TaskId.From(id);
        var updated = await taskService.UpdatePriorityAsync(taskId, (Priority)request.Priority, cancellationToken);
        if (!updated)
            return Results.NotFound();

        var task = await taskService.GetTaskAsync(taskId, cancellationToken);
        return Results.Ok(ToResponse(task!));
    }

    private static async Task<IResult> UpdateTaskStatusAsync(Guid id, UpdateStatusRequest request, TaskService taskService, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(TaskStatus), request.Status))
            return Results.BadRequest(new { error = "Invalid status value" });

        var taskId = TaskId.From(id);
        var updated = await taskService.UpdateTaskStatusAsync(taskId, (TaskStatus)request.Status, cancellationToken);
        if (!updated)
            return Results.NotFound();

        var task = await taskService.GetTaskAsync(taskId, cancellationToken);
        return Results.Ok(ToResponse(task!));
    }

    private static async Task<IResult> UpdateTaskAsync(Guid id, UpdateTaskRequest request, UpdateTaskCommandHandler updateTaskCommandHandler, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(Priority), request.Priority))
            return Results.BadRequest(new { error = "Invalid priority value" });

        try
        {
            var command = new UpdateTaskCommand
            {
                TaskId = id,
                Title = request.Title,
                Description = request.Description,
                Priority = (Priority)request.Priority,
                DueDate = request.DueDate
            };

            var task = await updateTaskCommandHandler.HandleAsync(command, cancellationToken);
            return task is null ? Results.NotFound() : Results.Ok(ToResponse(task));
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> GetTasksAsync(TaskService taskService, GetTasksQueryHandler getTasksQueryHandler, string? status, string? priority, string? sortBy, string? sortOrder, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(status))
        {
            if (!Enum.TryParse<TaskStatus>(status, ignoreCase: true, out var parsedStatus))
                return Results.BadRequest(new { error = $"Invalid status value: {status}" });

            var statusFilteredTasks = await getTasksQueryHandler.HandleAsync(new GetTasksQuery { Status = parsedStatus }, cancellationToken);
            return Results.Ok(statusFilteredTasks.Select(ToResponse));
        }

        IEnumerable<DomainTask> tasks;

        if (!string.IsNullOrWhiteSpace(priority))
        {
            var priorities = new List<Priority>();
            foreach (var segment in priority.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                if (!Enum.TryParse<Priority>(segment, ignoreCase: true, out var parsedPriority))
                    return Results.BadRequest(new { error = $"Invalid priority value: {segment}" });

                priorities.Add(parsedPriority);
            }

            tasks = await taskService.GetTasksByPriorityAsync(priorities, cancellationToken);
        }
        else
        {
            tasks = await taskService.GetActiveTasksAsync(cancellationToken);
        }

        if (string.Equals(sortBy, "priority", StringComparison.OrdinalIgnoreCase))
        {
            tasks = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase)
                ? tasks.OrderBy(t => t.Priority)
                : tasks.OrderByDescending(t => t.Priority);
        }

        return Results.Ok(tasks.Select(ToResponse));
    }

    private static TaskResponse ToResponse(DomainTask task) => new(
        task.Id.Value,
        task.Title,
        task.Description,
        (int)task.Status,
        (int)task.Priority,
        task.CreatedAt,
        task.UpdatedAt,
        task.DueDate);
}

