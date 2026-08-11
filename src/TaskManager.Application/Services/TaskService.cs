using Microsoft.Extensions.Logging;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;
using TaskStatus = TaskManager.Domain.Tasks.TaskStatus;
using System.Diagnostics;

namespace TaskManager.Application.Services;

/// <summary>
/// Application service for task management
/// This will be used in Lab 2: Requirements → Backlog → Code
/// 
/// Includes OpenTelemetry ActivitySource for observability demonstrations
/// </summary>
public sealed class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskService> _logger;
    private static readonly ActivitySource ActivitySource = new("TaskManager.Application");

    public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    /// <summary>
    /// Add a new task to the system
    /// 
    /// Note: Includes OpenTelemetry activity for observability demonstration
    /// </summary>
    public async System.Threading.Tasks.Task<TaskId> AddTaskAsync(string title, string description, Priority priority = Priority.Medium, CancellationToken cancellationToken = default)
    {
        using var activity = ActivitySource.StartActivity("TaskService.AddTask");
        activity?.SetTag("task.title", title);
        activity?.SetTag("operation", "create");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be null or empty", nameof(description));

        _logger.LogInformation("AddTaskAsync called with title: {Title}", title);

        var task = DomainTask.Create(title, description, priority);
        await _taskRepository.AddTaskAsync(task, cancellationToken);

        activity?.SetTag("task.id", task.Id.Value);

        return task.Id;
    }

    /// <summary>
    /// Get a task by its ID
    /// </summary>
    public System.Threading.Tasks.Task<DomainTask?> GetTaskAsync(TaskId taskId, CancellationToken cancellationToken = default)
    {
        return _taskRepository.FindByIdAsync(taskId, cancellationToken);
    }

    /// <summary>
    /// Change the priority of an existing task
    /// </summary>
    /// <returns>true if the task was found and updated; false if no task exists with the given id</returns>
    public async System.Threading.Tasks.Task<bool> UpdatePriorityAsync(TaskId taskId, Priority priority, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.FindByIdAsync(taskId, cancellationToken);
        if (task is null)
            return false;

        task.UpdatePriority(priority);
        await _taskRepository.SaveChangesAsync(task, cancellationToken);

        return true;
    }

    /// <summary>
    /// Update task status
    /// TODO: This method will be implemented during the workshop
    /// </summary>
    public System.Threading.Tasks.Task UpdateTaskStatusAsync(TaskId taskId, TaskStatus newStatus, CancellationToken cancellationToken = default)
    {
        // TODO: Participants will implement this during the workshop
        throw new NotImplementedException("This will be implemented during the workshop");
    }

    /// <summary>
    /// Get all active tasks
    /// </summary>
    public System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetActiveTasksAsync(CancellationToken cancellationToken = default)
    {
        return _taskRepository.GetActiveTasksAsync(cancellationToken);
    }

    /// <summary>
    /// Get active tasks matching any of the given priorities
    /// </summary>
    public System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetTasksByPriorityAsync(IEnumerable<Priority> priorities, CancellationToken cancellationToken = default)
    {
        return _taskRepository.FindTasksByPriorityAsync(priorities, cancellationToken);
    }
}
