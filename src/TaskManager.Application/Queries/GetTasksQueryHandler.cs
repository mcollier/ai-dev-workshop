using TaskManager.Domain.Repositories;
using DomainTask = TaskManager.Domain.Tasks.Task;
using TaskStatus = TaskManager.Domain.Tasks.TaskStatus;

namespace TaskManager.Application.Queries;

/// <summary>
/// Handles <see cref="GetTasksQuery"/> by retrieving and filtering tasks from the repository
/// </summary>
public sealed class GetTasksQueryHandler
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async System.Threading.Tasks.Task<IEnumerable<DomainTask>> HandleAsync(GetTasksQuery query, CancellationToken cancellationToken = default)
    {
        var tasks = await _taskRepository.GetAllTasksAsync(cancellationToken);

        if (query.Status is TaskStatus status)
            tasks = tasks.Where(t => t.Status == status);

        return tasks.OrderByDescending(t => t.CreatedAt);
    }
}
