using TaskStatus = TaskManager.Domain.Tasks.TaskStatus;

namespace TaskManager.Application.Queries;

/// <summary>
/// Query parameters for retrieving tasks, optionally filtered by status
/// </summary>
public sealed class GetTasksQuery
{
    /// <summary>
    /// The status to filter tasks by, or null to return tasks of any status.
    /// </summary>
    public TaskStatus? Status { get; init; }
}
