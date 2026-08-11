using TaskManager.Domain.Repositories;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;

namespace TaskManager.Application.Queries;

/// <summary>
/// Handles <see cref="GetTaskByIdQuery"/> by retrieving a single task from the repository
/// </summary>
public sealed class GetTaskByIdQueryHandler
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public System.Threading.Tasks.Task<DomainTask?> HandleAsync(GetTaskByIdQuery query, CancellationToken cancellationToken = default)
    {
        if (query.TaskId == Guid.Empty)
            throw new ArgumentException("Task id cannot be empty", nameof(query));

        return _taskRepository.FindByIdAsync(TaskId.From(query.TaskId), cancellationToken);
    }
}
