using FakeItEasy;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Services;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;
using Priority = TaskManager.Domain.Tasks.Priority;

namespace TaskManager.UnitTests.Services.TaskServiceTests;

public sealed class GetActiveTasksAsyncTests
{
    private readonly ITaskRepository _mockRepository;
    private readonly ILogger<TaskService> _mockLogger;
    private readonly TaskService _taskService;

    public GetActiveTasksAsyncTests()
    {
        _mockRepository = A.Fake<ITaskRepository>();
        _mockLogger = A.Fake<ILogger<TaskService>>();
        _taskService = new TaskService(_mockRepository, _mockLogger);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetActiveTasksAsync_DelegatesToRepository()
    {
        var tasks = new[] { DomainTask.Create("Finish report", "Complete the quarterly report") };
        A.CallTo(() => _mockRepository.GetActiveTasksAsync(A<CancellationToken>._)).Returns(tasks);

        var result = await _taskService.GetActiveTasksAsync(TestContext.Current.CancellationToken);

        Assert.Equal(tasks, result);
    }
}
