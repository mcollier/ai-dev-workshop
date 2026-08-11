using TaskManager.Domain.Tasks;
using TaskManager.Infrastructure.Repositories;
using DomainTask = TaskManager.Domain.Tasks.Task;

namespace TaskManager.IntegrationTests.Tasks;

public sealed class FindTasksByPriorityAsyncTests
{
    private readonly InMemoryTaskRepository _repository = new();

    [Fact]
    public async System.Threading.Tasks.Task FindTasksByPriorityAsync_WithSinglePriority_ReturnsOnlyMatchingTasks()
    {
        var highPriorityTask = DomainTask.Create("Ship release", "Deploy the release", Priority.High);
        var lowPriorityTask = DomainTask.Create("Water plants", "Weekly plant care", Priority.Low);
        await _repository.AddTaskAsync(highPriorityTask, TestContext.Current.CancellationToken);
        await _repository.AddTaskAsync(lowPriorityTask, TestContext.Current.CancellationToken);

        var results = (await _repository.FindTasksByPriorityAsync([Priority.High], TestContext.Current.CancellationToken)).ToList();

        Assert.Contains(results, t => t.Id == highPriorityTask.Id);
        Assert.DoesNotContain(results, t => t.Id == lowPriorityTask.Id);
    }

    [Fact]
    public async System.Threading.Tasks.Task FindTasksByPriorityAsync_WithMultiplePriorities_ReturnsAllMatchingTasks()
    {
        var highPriorityTask = DomainTask.Create("Ship release", "Deploy the release", Priority.High);
        var mediumPriorityTask = DomainTask.Create("Review PR", "Review pending pull request", Priority.Medium);
        var lowPriorityTask = DomainTask.Create("Water plants", "Weekly plant care", Priority.Low);
        await _repository.AddTaskAsync(highPriorityTask, TestContext.Current.CancellationToken);
        await _repository.AddTaskAsync(mediumPriorityTask, TestContext.Current.CancellationToken);
        await _repository.AddTaskAsync(lowPriorityTask, TestContext.Current.CancellationToken);

        var results = (await _repository.FindTasksByPriorityAsync([Priority.High, Priority.Medium], TestContext.Current.CancellationToken)).ToList();

        Assert.Contains(results, t => t.Id == highPriorityTask.Id);
        Assert.Contains(results, t => t.Id == mediumPriorityTask.Id);
        Assert.DoesNotContain(results, t => t.Id == lowPriorityTask.Id);
    }
}
