namespace TaskManager.Api.Tasks;

/// <summary>
/// Request body for updating a task's status
/// </summary>
public sealed record UpdateStatusRequest(int Status);
