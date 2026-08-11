using FakeItEasy;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Services;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;
using Priority = TaskManager.Domain.Tasks.Priority;

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
    public System.Threading.Tasks.Task UpdateTaskStatusAsync_WithValidData_ShouldUpdateSuccessfully()
    {
        // TODO: Lab 4 - Generate this test with Copilot
        throw new NotImplementedException("Lab 4: Generate this test with Copilot assistance");
    }
}
