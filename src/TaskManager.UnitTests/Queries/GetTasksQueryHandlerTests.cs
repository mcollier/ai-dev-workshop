using FakeItEasy;
using TaskManager.Application.Queries;
using TaskManager.Domain.Repositories;
using DomainTask = TaskManager.Domain.Tasks.Task;
using TaskStatus = TaskManager.Domain.Tasks.TaskStatus;

namespace TaskManager.UnitTests.Queries;

public sealed class GetTasksQueryHandlerTests
{
    private readonly ITaskRepository _mockRepository;
    private readonly GetTasksQueryHandler _handler;

    public GetTasksQueryHandlerTests()
    {
        _mockRepository = A.Fake<ITaskRepository>();
        _handler = new GetTasksQueryHandler(_mockRepository);
    }

    [Fact]
    public async System.Threading.Tasks.Task HandleAsync_WithoutStatusFilter_ReturnsAllTasksOrderedByCreatedAtDescending()
    {
        var first = DomainTask.Create("First", "Created first");
        await System.Threading.Tasks.Task.Delay(5, TestContext.Current.CancellationToken);
        var second = DomainTask.Create("Second", "Created second");
        await System.Threading.Tasks.Task.Delay(5, TestContext.Current.CancellationToken);
        var third = DomainTask.Create("Third", "Created third");

        A.CallTo(() => _mockRepository.GetAllTasksAsync(A<CancellationToken>._))
            .Returns(new[] { first, second, third });

        var result = await _handler.HandleAsync(new GetTasksQuery(), TestContext.Current.CancellationToken);

        Assert.Equal(new[] { third, second, first }, result);
    }

    [Fact]
    public async System.Threading.Tasks.Task HandleAsync_WithStatusFilter_ReturnsOnlyMatchingTasks()
    {
        var todoTask = DomainTask.Create("Todo task", "Still pending");
        var doneTask = DomainTask.Create("Done task", "Finished");
        doneTask.UpdateStatus(TaskStatus.Done);

        A.CallTo(() => _mockRepository.GetAllTasksAsync(A<CancellationToken>._))
            .Returns(new[] { todoTask, doneTask });

        var result = await _handler.HandleAsync(new GetTasksQuery { Status = TaskStatus.Done }, TestContext.Current.CancellationToken);

        Assert.Equal(new[] { doneTask }, result);
    }

    [Fact]
    public async System.Threading.Tasks.Task HandleAsync_DelegatesToRepository()
    {
        A.CallTo(() => _mockRepository.GetAllTasksAsync(A<CancellationToken>._))
            .Returns(Array.Empty<DomainTask>());

        await _handler.HandleAsync(new GetTasksQuery(), TestContext.Current.CancellationToken);

        A.CallTo(() => _mockRepository.GetAllTasksAsync(A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }
}
