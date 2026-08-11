using FakeItEasy;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Services;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;
using Priority = TaskManager.Domain.Tasks.Priority;

namespace TaskManager.UnitTests.Services.TaskServiceTests;

public sealed class UpdatePriorityAsyncTests
{
    private readonly ITaskRepository _mockRepository;
    private readonly ILogger<TaskService> _mockLogger;
    private readonly TaskService _taskService;

    public UpdatePriorityAsyncTests()
    {
        _mockRepository = A.Fake<ITaskRepository>();
        _mockLogger = A.Fake<ILogger<TaskService>>();
        _taskService = new TaskService(_mockRepository, _mockLogger);
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdatePriorityAsync_WithExistingTask_UpdatesPriorityAndReturnsTrue()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");
        A.CallTo(() => _mockRepository.FindByIdAsync(task.Id, A<CancellationToken>._)).Returns(task);

        var result = await _taskService.UpdatePriorityAsync(task.Id, Priority.High);

        Assert.True(result);
        Assert.Equal(Priority.High, task.Priority);
        A.CallTo(() => _mockRepository.SaveChangesAsync(task, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdatePriorityAsync_WithNonExistentTask_ReturnsFalse()
    {
        var taskId = TaskId.New();
        A.CallTo(() => _mockRepository.FindByIdAsync(taskId, A<CancellationToken>._)).Returns((DomainTask?)null);

        var result = await _taskService.UpdatePriorityAsync(taskId, Priority.High);

        Assert.False(result);
        A.CallTo(() => _mockRepository.SaveChangesAsync(A<DomainTask>._, A<CancellationToken>._)).MustNotHaveHappened();
    }
}
