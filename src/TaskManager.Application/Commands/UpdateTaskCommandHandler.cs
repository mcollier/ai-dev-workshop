using TaskManager.Domain.Repositories;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;

namespace TaskManager.Application.Commands;

/// <summary>
/// Handles <see cref="UpdateTaskCommand"/> by applying changes to an existing task
/// </summary>
public sealed class UpdateTaskCommandHandler
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskCommandHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The repository used to retrieve and persist tasks.</param>
    public UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the command by applying the requested changes to an existing task.
    /// </summary>
    /// <param name="command">The command containing the task id and updated values.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>the updated task, or null if no task exists with the given id</returns>
    /// <exception cref="ArgumentException">Thrown when the title or description is null, empty, or whitespace, or when the due date is not in the future.</exception>
    public async System.Threading.Tasks.Task<DomainTask?> HandleAsync(UpdateTaskCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            throw new ArgumentException("Title cannot be null or empty", nameof(command));

        if (string.IsNullOrWhiteSpace(command.Description))
            throw new ArgumentException("Description cannot be null or empty", nameof(command));

        var task = await _taskRepository.FindByIdAsync(TaskId.From(command.TaskId), cancellationToken);
        if (task is null)
            return null;

        task.UpdateDetails(command.Title, command.Description);
        task.UpdatePriority(command.Priority);
        task.SetDueDate(command.DueDate);

        await _taskRepository.SaveChangesAsync(task, cancellationToken);

        return task;
    }
}
