using TaskStatus = TaskManager.Domain.Tasks.TaskStatus;

namespace TaskManager.Application.Queries;

/// <summary>
/// Query parameters for retrieving tasks, optionally filtered by status
/// </summary>
public sealed class GetTasksQuery
{
    public TaskStatus? Status { get; init; }
}
