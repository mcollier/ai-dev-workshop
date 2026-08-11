using FakeItEasy;
using TaskManager.Application.Commands;
using TaskManager.Domain.Repositories;
using DomainTask = TaskManager.Domain.Tasks.Task;
using Priority = TaskManager.Domain.Tasks.Priority;
using TaskId = TaskManager.Domain.Tasks.TaskId;

namespace TaskManager.UnitTests.Commands;

public sealed class UpdateTaskCommandHandlerTests
{
    private readonly ITaskRepository _mockRepository;
    private readonly UpdateTaskCommandHandler _handler;

    public UpdateTaskCommandHandlerTests()
    {
        _mockRepository = A.Fake<ITaskRepository>();
        _handler = new UpdateTaskCommandHandler(_mockRepository);
    }

    [Fact]
    public async System.Threading.Tasks.Task HandleAsync_WithExistingTask_UpdatesAndReturnsTask()
    {
        var task = DomainTask.Create("Original title", "Original description");
        A.CallTo(() => _mockRepository.FindByIdAsync(task.Id, A<CancellationToken>._)).Returns(task);
        var dueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7));

        var command = new UpdateTaskCommand
        {
            TaskId = task.Id.Value,
            Title = "Updated title",
            Description = "Updated description",
            Priority = Priority.High,
            DueDate = dueDate
        };

        var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Equal("Updated title", result!.Title);
        Assert.Equal("Updated description", result.Description);
        Assert.Equal(Priority.High, result.Priority);
        Assert.Equal(dueDate, result.DueDate);
        A.CallTo(() => _mockRepository.SaveChangesAsync(task, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async System.Threading.Tasks.Task HandleAsync_WithNonExistentTask_ReturnsNull()
    {
        var taskId = TaskId.New();
        A.CallTo(() => _mockRepository.FindByIdAsync(taskId, A<CancellationToken>._)).Returns((DomainTask?)null);

        var command = new UpdateTaskCommand
        {
            TaskId = taskId.Value,
            Title = "Updated title",
            Description = "Updated description",
            Priority = Priority.High
        };

        var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Theory]
    [InlineData("", "Valid description")]
    [InlineData("Valid title", "")]
    public async System.Threading.Tasks.Task HandleAsync_WithInvalidTitleOrDescription_ThrowsArgumentException(string title, string description)
    {
        var command = new UpdateTaskCommand
        {
            TaskId = Guid.NewGuid(),
            Title = title,
            Description = description,
            Priority = Priority.Medium
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.HandleAsync(command, TestContext.Current.CancellationToken));
    }
}
