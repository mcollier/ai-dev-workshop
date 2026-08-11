using FakeItEasy;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Services;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;
using Priority = TaskManager.Domain.Tasks.Priority;
using TaskStatus = TaskManager.Domain.Tasks.TaskStatus;

namespace TaskManager.UnitTests;

/// <summary>
/// Unit tests for TaskService - Lab 2 & Lab 4
/// 
/// LAB 2 INSTRUCTIONS:
/// Use Copilot to implement the TaskService.AddTaskAsync method and its tests.
/// 
/// LAB 4 INSTRUCTIONS: 
/// Use Copilot to generate additional unit tests covering edge cases.
/// 
/// Example prompts:
/// - "Implement the AddTaskAsync method with validation and logging"
/// - "Generate unit tests for happy path and validation scenarios"
/// - "Add tests for null parameters and empty strings"
/// </summary>
public class TaskServiceTests
{
    private readonly ITaskRepository _mockRepository;
    private readonly ILogger<TaskService> _mockLogger;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        // Setup mocks using FakeItEasy
        _mockRepository = A.Fake<ITaskRepository>();
        _mockLogger = A.Fake<ILogger<TaskService>>();
        _taskService = new TaskService(_mockRepository, _mockLogger);
    }

    [Fact]
    public async System.Threading.Tasks.Task AddTaskAsync_WithValidData_ShouldReturnTaskId()
    {
        const string title = "Finish report";
        const string description = "Complete the quarterly report";

        var taskId = await _taskService.AddTaskAsync(title, description);

        Assert.NotNull(taskId);
        A.CallTo(() => _mockRepository.AddTaskAsync(
                A<DomainTask>.That.Matches(t => t.Title == title && t.Description == description && t.Priority == Priority.Medium),
                A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async System.Threading.Tasks.Task AddTaskAsync_WithInvalidTitle_ShouldThrowArgumentException(string invalidTitle)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _taskService.AddTaskAsync(invalidTitle, "description"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async System.Threading.Tasks.Task AddTaskAsync_WithInvalidDescription_ShouldThrowArgumentException(string invalidDescription)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _taskService.AddTaskAsync("title", invalidDescription));
    }

    [Theory]
    [InlineData(Priority.Low)]
    [InlineData(Priority.Medium)]
    [InlineData(Priority.High)]
    public async System.Threading.Tasks.Task AddTaskAsync_WithExplicitPriority_ShouldPersistThatPriority(Priority priority)
    {
        await _taskService.AddTaskAsync("Finish report", "Complete the quarterly report", priority);

        A.CallTo(() => _mockRepository.AddTaskAsync(
                A<DomainTask>.That.Matches(t => t.Priority == priority),
                A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async System.Threading.Tasks.Task AddTaskAsync_ReturnsIdOfPersistedTask()
    {
        DomainTask? persistedTask = null;
        A.CallTo(() => _mockRepository.AddTaskAsync(A<DomainTask>._, A<CancellationToken>._))
            .Invokes((DomainTask task, CancellationToken _) => persistedTask = task);

        var taskId = await _taskService.AddTaskAsync("Finish report", "Complete the quarterly report");

        Assert.Equal(persistedTask!.Id, taskId);
    }

    [Fact]
    public async System.Threading.Tasks.Task AddTaskAsync_WhenCalled_LogsInformationMessage()
    {
        await _taskService.AddTaskAsync("Finish report", "Complete the quarterly report");

        A.CallTo(_mockLogger)
            .Where(call => call.Method.Name == "Log" && call.GetArgument<LogLevel>(0) == LogLevel.Information)
            .MustHaveHappened();
    }

    [Fact]
    public async System.Threading.Tasks.Task AddTaskAsync_PassesCancellationTokenToRepository()
    {
        using var cancellationTokenSource = new CancellationTokenSource();

        await _taskService.AddTaskAsync("Finish report", "Complete the quarterly report", cancellationToken: cancellationTokenSource.Token);

        A.CallTo(() => _mockRepository.AddTaskAsync(A<DomainTask>._, cancellationTokenSource.Token))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTaskAsync_WithExistingId_ShouldReturnTask()
    {
        var task = DomainTask.Create("title", "description");
        A.CallTo(() => _mockRepository.FindByIdAsync(task.Id, A<CancellationToken>._)).Returns(task);

        var result = await _taskService.GetTaskAsync(task.Id);

        Assert.Equal(task, result);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTaskAsync_WithNonExistentId_ShouldReturnNull()
    {
        var taskId = TaskId.New();
        A.CallTo(() => _mockRepository.FindByIdAsync(taskId, A<CancellationToken>._)).Returns((DomainTask?)null);

        var result = await _taskService.GetTaskAsync(taskId);

        Assert.Null(result);
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateTaskStatusAsync_WithValidData_ShouldUpdateSuccessfully()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");
        A.CallTo(() => _mockRepository.FindByIdAsync(task.Id, A<CancellationToken>._)).Returns(task);

        var updated = await _taskService.UpdateTaskStatusAsync(task.Id, TaskStatus.InProgress);

        Assert.True(updated);
        Assert.Equal(TaskStatus.InProgress, task.Status);
        A.CallTo(() => _mockRepository.SaveChangesAsync(task, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateTaskStatusAsync_WithNonExistentTask_ShouldReturnFalse()
    {
        var taskId = TaskId.New();
        A.CallTo(() => _mockRepository.FindByIdAsync(taskId, A<CancellationToken>._)).Returns((DomainTask?)null);

        var updated = await _taskService.UpdateTaskStatusAsync(taskId, TaskStatus.InProgress);

        Assert.False(updated);
    }
}
