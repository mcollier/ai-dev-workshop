namespace TaskManager.Api.Tasks;

/// <summary>
/// Request body for creating a new task
/// </summary>
public sealed record CreateTaskRequest(string Title, string Description, int? Priority = null);
