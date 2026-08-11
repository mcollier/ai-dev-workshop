using FakeItEasy;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Notifications;

namespace TaskManager.UnitTests.Services.NotificationServiceTests;

public sealed class SendSmsNotificationAsyncTests
{
    private readonly ILogger<NotificationService> _logger;
    private readonly NotificationService _sut;

    public SendSmsNotificationAsyncTests()
    {
        _logger = A.Fake<ILogger<NotificationService>>();
        _sut = new NotificationService(_logger);
    }

    [Fact]
    public async Task SendSmsNotificationAsync_WithValidInputs_SendsSms()
    {
        const string phoneNumber = "+15551234567";
        const string taskTitle = "Finish report";
        const string taskDescription = "Complete the quarterly report";

        await _sut.SendSmsNotificationAsync(phoneNumber, taskTitle, taskDescription);

        A.CallTo(_logger).Where(call =>
            call.Method.Name == "Log" &&
            call.GetArgument<LogLevel>(0) == LogLevel.Information)
            .MustHaveHappened();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendSmsNotificationAsync_WithInvalidPhoneNumber_ThrowsArgumentException(string invalidPhoneNumber)
    {
        const string taskTitle = "Finish report";
        const string taskDescription = "Complete the quarterly report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendSmsNotificationAsync(invalidPhoneNumber, taskTitle, taskDescription));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendSmsNotificationAsync_WithInvalidTaskTitle_ThrowsArgumentException(string invalidTaskTitle)
    {
        const string phoneNumber = "+15551234567";
        const string taskDescription = "Complete the quarterly report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendSmsNotificationAsync(phoneNumber, invalidTaskTitle, taskDescription));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SendSmsNotificationAsync_WithInvalidTaskDescription_ThrowsArgumentException(string invalidTaskDescription)
    {
        const string phoneNumber = "+15551234567";
        const string taskTitle = "Finish report";

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.SendSmsNotificationAsync(phoneNumber, taskTitle, invalidTaskDescription));
    }
}
