using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Notifications;

public sealed class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task SendEmailNotificationAsync(
        string email,
        string taskTitle,
        string taskDescription,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        if (string.IsNullOrWhiteSpace(taskTitle))
            throw new ArgumentException("Task title cannot be null or empty", nameof(taskTitle));

        if (string.IsNullOrWhiteSpace(taskDescription))
            throw new ArgumentException("Task description cannot be null or empty", nameof(taskDescription));

        _logger.LogInformation(
            "Sending email notification to {Email} for task {TaskTitle}",
            email,
            taskTitle);

        // Simulate email sending
        await Task.Delay(100, cancellationToken);

        _logger.LogInformation(
            "Email notification sent successfully to {Email}",
            email);
    }

    public async Task SendSmsNotificationAsync(
        string phoneNumber,
        string taskTitle,
        string taskDescription,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(taskTitle))
            throw new ArgumentException("Task title cannot be null or empty", nameof(taskTitle));

        if (string.IsNullOrWhiteSpace(taskDescription))
            throw new ArgumentException("Task description cannot be null or empty", nameof(taskDescription));

        _logger.LogInformation(
            "Sending SMS notification to {PhoneNumber} for task {TaskTitle}",
            phoneNumber,
            taskTitle);

        // Simulate SMS sending
        await Task.Delay(100, cancellationToken);

        _logger.LogInformation(
            "SMS notification sent successfully to {PhoneNumber}",
            phoneNumber);
    }

    public async Task SendCombinedNotificationAsync(
        string email,
        string phoneNumber,
        string taskTitle,
        string taskDescription,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(taskTitle))
            throw new ArgumentException("Task title cannot be null or empty", nameof(taskTitle));

        if (string.IsNullOrWhiteSpace(taskDescription))
            throw new ArgumentException("Task description cannot be null or empty", nameof(taskDescription));

        _logger.LogInformation(
            "Sending combined notification to email {Email} and phone {PhoneNumber}",
            email,
            phoneNumber);

        await SendEmailNotificationAsync(email, taskTitle, taskDescription, cancellationToken);
        await SendSmsNotificationAsync(phoneNumber, taskTitle, taskDescription, cancellationToken);

        _logger.LogInformation("Combined notification sent successfully");
    }
}
