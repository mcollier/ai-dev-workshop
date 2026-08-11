using TaskManager.Domain.Tasks;

namespace TaskManager.Application.Commands;

/// <summary>
/// Command to update an existing task's title, description, priority, and due date
/// </summary>
public sealed class UpdateTaskCommand
{
    public required Guid TaskId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required Priority Priority { get; init; }
    public DateOnly? DueDate { get; init; }
}
