namespace TaskManager.Domain.Tasks;

/// <summary>
/// Task aggregate root representing a work item to be completed
/// </summary>
public sealed class Task
{
    private Task(TaskId id, string title, string description, TaskStatus status, Priority priority, DateTime createdAt, DateOnly? dueDate)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
        DueDate = dueDate;
    }

    public TaskId Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatus Status { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }
    public DateOnly? DueDate { get; private set; }

    /// <summary>
    /// Factory method to create a new task
    /// </summary>
    public static Task Create(string title, string description, Priority priority = Priority.Medium, DateOnly? dueDate = null)
    {
        // TODO: Add validation (title not null/empty, description not null)
        // This is where Copilot will help participants implement validation
        ValidateDueDate(dueDate);

        return new Task(
            TaskId.New(),
            title,
            description,
            TaskStatus.Todo,
            priority,
            DateTime.UtcNow,
            dueDate);
    }

    /// <summary>
    /// Business method to update task status
    /// </summary>
    public void UpdateStatus(TaskStatus newStatus)
    {
        // TODO: Add business rules (e.g., can't move from Done to Todo directly)
        // This will be implemented during the workshop
        
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Business method to update task details
    /// </summary>
    public void UpdateDetails(string title, string description)
    {
        // TODO: Add validation
        // This will be implemented during the workshop
        
        Title = title;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Business method to change task priority
    /// </summary>
    public void UpdatePriority(Priority priority)
    {
        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Business method to set or clear the task's due date
    /// </summary>
    public void SetDueDate(DateOnly? dueDate)
    {
        ValidateDueDate(dueDate);

        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ValidateDueDate(DateOnly? dueDate)
    {
        if (dueDate is DateOnly value && value <= DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Due date must be in the future", nameof(dueDate));
    }
}
