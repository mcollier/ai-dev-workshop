using FakeItEasy;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Notifications;

namespace TaskManager.UnitTests.Services.NotificationServiceTests;

public sealed class SendEmailNotificationAsyncTests
{
    private readonly ILogger<NotificationService> _logger;
    private readonly NotificationService _sut;

    public SendEmailNotificationAsyncTests()
    {
        _logger = A.Fake<ILogger<NotificationService>>();
        _sut = new NotificationService(_logger);
    }

    [Fact]
    public async Task SendEmailNotificationAsync_WithValidInputs_SendsEmail()
    {
        const string email = "user@example.com";
        const string taskTitle = "Finish report";
        const string taskDescription = "Complete the quarterly report";

        await _sut.SendEmailNotificationAsync(email, taskTitle, taskDescription, TestContext.Current.CancellationToken);

        A.CallTo(_logger).Where(call =>
            call.Method.Name == "Log" &&
            call.GetArgument<LogLevel>(0) == LogLevel.Information)
            .MustHaveHappened();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendEmailNotificationAsync_WithInvalidEmail_ThrowsArgumentException(string? invalidEmail)
    {
        const string taskTitle = "Finish report";
        const string taskDescription = "Complete the quarterly report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendEmailNotificationAsync(invalidEmail!, taskTitle, taskDescription, TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendEmailNotificationAsync_WithInvalidTaskTitle_ThrowsArgumentException(string? invalidTaskTitle)
    {
        const string email = "user@example.com";
        const string taskDescription = "Complete the quarterly report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendEmailNotificationAsync(email, invalidTaskTitle!, taskDescription, TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendEmailNotificationAsync_WithInvalidTaskDescription_ThrowsArgumentException(string? invalidTaskDescription)
    {
        const string email = "user@example.com";
        const string taskTitle = "Finish report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendEmailNotificationAsync(email, taskTitle, invalidTaskDescription!, TestContext.Current.CancellationToken));
    }
}
