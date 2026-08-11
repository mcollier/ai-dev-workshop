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

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The repository used to retrieve tasks.</param>
    public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the query by retrieving a single task from the repository.
    /// </summary>
    /// <param name="query">The query specifying which task to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The matching task, or null if no task exists with the given id.</returns>
    /// <exception cref="ArgumentException">Thrown when <see cref="GetTaskByIdQuery.TaskId"/> is <see cref="Guid.Empty"/>.</exception>
    public System.Threading.Tasks.Task<DomainTask?> HandleAsync(GetTaskByIdQuery query, CancellationToken cancellationToken = default)
    {
        if (query.TaskId == Guid.Empty)
            throw new ArgumentException("Task id cannot be empty", nameof(query));

        return _taskRepository.FindByIdAsync(TaskId.From(query.TaskId), cancellationToken);
    }
}
