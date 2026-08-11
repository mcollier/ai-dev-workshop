using FakeItEasy;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Notifications;

namespace TaskManager.UnitTests.Services.NotificationServiceTests;

public sealed class SendCombinedNotificationAsyncTests
{
    private readonly ILogger<NotificationService> _logger;
    private readonly NotificationService _sut;

    public SendCombinedNotificationAsyncTests()
    {
        _logger = A.Fake<ILogger<NotificationService>>();
        _sut = new NotificationService(_logger);
    }

    [Fact]
    public async Task SendCombinedNotificationAsync_WithValidInputs_SendsEmailAndSms()
    {
        const string email = "user@example.com";
        const string phoneNumber = "+15551234567";
        const string taskTitle = "Finish report";
        const string taskDescription = "Complete the quarterly report";

        await _sut.SendCombinedNotificationAsync(email, phoneNumber, taskTitle, taskDescription);

        A.CallTo(_logger).Where(call =>
            call.Method.Name == "Log" &&
            call.GetArgument<LogLevel>(0) == LogLevel.Information)
            .MustHaveHappened();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendCombinedNotificationAsync_WithInvalidEmail_ThrowsArgumentException(string invalidEmail)
    {
        const string phoneNumber = "+15551234567";
        const string taskTitle = "Finish report";
        const string taskDescription = "Complete the quarterly report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendCombinedNotificationAsync(invalidEmail, phoneNumber, taskTitle, taskDescription));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendCombinedNotificationAsync_WithInvalidPhoneNumber_ThrowsArgumentException(string invalidPhoneNumber)
    {
        const string email = "user@example.com";
        const string taskTitle = "Finish report";
        const string taskDescription = "Complete the quarterly report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendCombinedNotificationAsync(email, invalidPhoneNumber, taskTitle, taskDescription));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendCombinedNotificationAsync_WithInvalidTaskTitle_ThrowsArgumentException(string invalidTaskTitle)
    {
        const string email = "user@example.com";
        const string phoneNumber = "+15551234567";
        const string taskDescription = "Complete the quarterly report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendCombinedNotificationAsync(email, phoneNumber, invalidTaskTitle, taskDescription));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendCombinedNotificationAsync_WithInvalidTaskDescription_ThrowsArgumentException(string invalidTaskDescription)
    {
        const string email = "user@example.com";
        const string phoneNumber = "+15551234567";
        const string taskTitle = "Finish report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendCombinedNotificationAsync(email, phoneNumber, taskTitle, invalidTaskDescription));
    }
}
