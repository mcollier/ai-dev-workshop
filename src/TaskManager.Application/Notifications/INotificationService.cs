namespace TaskManager.Application.Notifications;

/// <summary>
/// Port for sending task-related notifications via email and SMS
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Send an email notification about a task
    /// </summary>
    Task SendEmailNotificationAsync(string email, string taskTitle, string taskDescription, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send an SMS notification about a task
    /// </summary>
    Task SendSmsNotificationAsync(string phoneNumber, string taskTitle, string taskDescription, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send both an email and an SMS notification about the same task
    /// </summary>
    Task SendCombinedNotificationAsync(string email, string phoneNumber, string taskTitle, string taskDescription, CancellationToken cancellationToken = default);
}
