namespace TaskManager.Api.Tasks;

/// <summary>
/// Request body for updating a task's title, description, priority, and due date
/// </summary>
public sealed record UpdateTaskRequest(string Title, string Description, int Priority, DateOnly? DueDate = null);
