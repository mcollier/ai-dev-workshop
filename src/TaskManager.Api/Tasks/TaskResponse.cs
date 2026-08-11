namespace TaskManager.Api.Tasks;

/// <summary>
/// API representation of a task
/// </summary>
public sealed record TaskResponse(Guid Id, string Title, string Description, int Status, int Priority, DateTime CreatedAt, DateTime UpdatedAt);
