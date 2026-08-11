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

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskService"/> class.
    /// </summary>
    /// <param name="taskRepository">The repository used to persist and retrieve tasks.</param>
    /// <param name="logger">The logger used for structured logging.</param>
    public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    /// <summary>
    /// Adds a new task to the system.
    /// Includes an OpenTelemetry activity for observability demonstration.
    /// </summary>
    /// <param name="title">The task title. Cannot be null, empty, or whitespace.</param>
    /// <param name="description">The task description. Cannot be null, empty, or whitespace.</param>
    /// <param name="priority">The task priority. Defaults to <see cref="Priority.Medium"/>.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The id of the newly created task.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="title"/> or <paramref name="description"/> is null, empty, or whitespace.</exception>
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
    /// Gets a task by its id.
    /// </summary>
    /// <param name="taskId">The id of the task to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The matching task, or null if no task exists with the given id.</returns>
    public System.Threading.Tasks.Task<DomainTask?> GetTaskAsync(TaskId taskId, CancellationToken cancellationToken = default)
    {
        return _taskRepository.FindByIdAsync(taskId, cancellationToken);
    }

    /// <summary>
    /// Changes the priority of an existing task.
    /// </summary>
    /// <param name="taskId">The id of the task to update.</param>
    /// <param name="priority">The new priority to assign.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>true if the task was found and updated; false if no task exists with the given id.</returns>
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
    /// Changes the status of an existing task.
    /// </summary>
    /// <param name="taskId">The id of the task to update.</param>
    /// <param name="newStatus">The new status to assign.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>true if the task was found and updated; false if no task exists with the given id.</returns>
    public async System.Threading.Tasks.Task<bool> UpdateTaskStatusAsync(TaskId taskId, TaskStatus newStatus, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.FindByIdAsync(taskId, cancellationToken);
        if (task is null)
            return false;

        task.UpdateStatus(newStatus);
        await _taskRepository.SaveChangesAsync(task, cancellationToken);

        return true;
    }

    /// <summary>
    /// Gets all active tasks (tasks that are not <see cref="TaskStatus.Done"/> or <see cref="TaskStatus.Cancelled"/>).
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The set of active tasks.</returns>
    public System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetActiveTasksAsync(CancellationToken cancellationToken = default)
    {
        return _taskRepository.GetActiveTasksAsync(cancellationToken);
    }

    /// <summary>
    /// Gets active tasks matching any of the given priorities.
    /// </summary>
    /// <param name="priorities">The set of priorities to match.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The active tasks matching any of the given priorities.</returns>
    public System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetTasksByPriorityAsync(IEnumerable<Priority> priorities, CancellationToken cancellationToken = default)
    {
        return _taskRepository.FindTasksByPriorityAsync(priorities, cancellationToken);
    }
}
