namespace TaskManager.Api.Tasks;

/// <summary>
/// Request body for updating a task's priority
/// </summary>
public sealed record UpdatePriorityRequest(int Priority);
