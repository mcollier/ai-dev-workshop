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

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTasksQueryHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The repository used to retrieve tasks.</param>
    public GetTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the query by retrieving tasks, optionally filtering by status, ordered by creation date descending.
    /// </summary>
    /// <param name="query">The query specifying the optional status filter.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The matching tasks, ordered by <c>CreatedAt</c> descending.</returns>
    public async System.Threading.Tasks.Task<IEnumerable<DomainTask>> HandleAsync(GetTasksQuery query, CancellationToken cancellationToken = default)
    {
        var tasks = await _taskRepository.GetAllTasksAsync(cancellationToken);

        if (query.Status is TaskStatus status)
            tasks = tasks.Where(t => t.Status == status);

        return tasks.OrderByDescending(t => t.CreatedAt);
    }
}
