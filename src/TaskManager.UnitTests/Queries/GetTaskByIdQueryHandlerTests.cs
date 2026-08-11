using FakeItEasy;
using TaskManager.Application.Queries;
using TaskManager.Domain.Repositories;
using DomainTask = TaskManager.Domain.Tasks.Task;
using TaskId = TaskManager.Domain.Tasks.TaskId;

namespace TaskManager.UnitTests.Queries;

public sealed class GetTaskByIdQueryHandlerTests
{
    private readonly ITaskRepository _mockRepository;
    private readonly GetTaskByIdQueryHandler _handler;

    public GetTaskByIdQueryHandlerTests()
    {
        _mockRepository = A.Fake<ITaskRepository>();
        _handler = new GetTaskByIdQueryHandler(_mockRepository);
    }

    [Fact]
    public async System.Threading.Tasks.Task HandleAsync_WithExistingTaskId_ReturnsTask()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");
        A.CallTo(() => _mockRepository.FindByIdAsync(task.Id, A<CancellationToken>._)).Returns(task);

        var result = await _handler.HandleAsync(new GetTaskByIdQuery { TaskId = task.Id.Value }, TestContext.Current.CancellationToken);

        Assert.Equal(task, result);
    }

    [Fact]
    public async System.Threading.Tasks.Task HandleAsync_WithNonExistentTaskId_ReturnsNull()
    {
        var taskId = TaskId.New();
        A.CallTo(() => _mockRepository.FindByIdAsync(taskId, A<CancellationToken>._)).Returns((DomainTask?)null);

        var result = await _handler.HandleAsync(new GetTaskByIdQuery { TaskId = taskId.Value }, TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async System.Threading.Tasks.Task HandleAsync_WithEmptyTaskId_ThrowsArgumentException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.HandleAsync(new GetTaskByIdQuery { TaskId = Guid.Empty }, TestContext.Current.CancellationToken));
    }
}
