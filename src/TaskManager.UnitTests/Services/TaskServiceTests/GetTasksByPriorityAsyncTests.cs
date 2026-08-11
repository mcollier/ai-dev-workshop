using FakeItEasy;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Services;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;
using Priority = TaskManager.Domain.Tasks.Priority;

namespace TaskManager.UnitTests.Services.TaskServiceTests;

public sealed class GetTasksByPriorityAsyncTests
{
    private readonly ITaskRepository _mockRepository;
    private readonly ILogger<TaskService> _mockLogger;
    private readonly TaskService _taskService;

    public GetTasksByPriorityAsyncTests()
    {
        _mockRepository = A.Fake<ITaskRepository>();
        _mockLogger = A.Fake<ILogger<TaskService>>();
        _taskService = new TaskService(_mockRepository, _mockLogger);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTasksByPriorityAsync_DelegatesToRepositoryWithGivenPriorities()
    {
        Priority[] priorities = [Priority.High, Priority.Medium];
        var tasks = new[] { DomainTask.Create("Ship release", "Deploy the release", Priority.High) };
        A.CallTo(() => _mockRepository.FindTasksByPriorityAsync(priorities, A<CancellationToken>._)).Returns(tasks);

        var result = await _taskService.GetTasksByPriorityAsync(priorities);

        Assert.Equal(tasks, result);
    }
}
