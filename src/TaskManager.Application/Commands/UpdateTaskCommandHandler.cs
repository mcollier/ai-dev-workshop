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

    public UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <returns>the updated task, or null if no task exists with the given id</returns>
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
