namespace TaskManager.Application.Queries;

/// <summary>
/// Query to retrieve a single task by its id
/// </summary>
public sealed class GetTaskByIdQuery
{
    /// <summary>
    /// The id of the task to retrieve.
    /// </summary>
    public required Guid TaskId { get; init; }
}
