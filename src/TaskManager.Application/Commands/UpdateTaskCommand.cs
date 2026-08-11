using TaskManager.Domain.Tasks;

namespace TaskManager.Application.Commands;

/// <summary>
/// Command to update an existing task's title, description, priority, and due date
/// </summary>
public sealed class UpdateTaskCommand
{
    /// <summary>
    /// The id of the task to update.
    /// </summary>
    public required Guid TaskId { get; init; }

    /// <summary>
    /// The new title. Cannot be null, empty, or whitespace.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// The new description. Cannot be null, empty, or whitespace.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// The new priority.
    /// </summary>
    public required Priority Priority { get; init; }

    /// <summary>
    /// The new due date, or null to clear it. Must be in the future when provided.
    /// </summary>
    public DateOnly? DueDate { get; init; }
}
