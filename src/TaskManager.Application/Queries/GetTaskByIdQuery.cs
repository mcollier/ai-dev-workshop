namespace TaskManager.Application.Queries;

/// <summary>
/// Query to retrieve a single task by its id
/// </summary>
public sealed class GetTaskByIdQuery
{
    public required Guid TaskId { get; init; }
}
