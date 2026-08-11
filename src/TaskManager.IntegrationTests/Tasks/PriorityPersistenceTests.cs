using TaskManager.Domain.Tasks;
using TaskManager.Infrastructure.Repositories;
using DomainTask = TaskManager.Domain.Tasks.Task;

namespace TaskManager.IntegrationTests.Tasks;

public sealed class PriorityPersistenceTests
{
    private readonly InMemoryTaskRepository _repository = new();

    [Fact]
    public async System.Threading.Tasks.Task AddTaskAsync_WithPriority_PersistsPriority()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report", Priority.High);

        await _repository.AddTaskAsync(task, TestContext.Current.CancellationToken);
        var persisted = await _repository.FindByIdAsync(task.Id, TestContext.Current.CancellationToken);

        Assert.NotNull(persisted);
        Assert.Equal(Priority.High, persisted.Priority);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetActiveTasksAsync_ReturnsTasksWithPriorityIntact()
    {
        var lowPriorityTask = DomainTask.Create("Water plants", "Weekly plant care", Priority.Low);
        var highPriorityTask = DomainTask.Create("Ship release", "Deploy the release", Priority.High);

        await _repository.AddTaskAsync(lowPriorityTask, TestContext.Current.CancellationToken);
        await _repository.AddTaskAsync(highPriorityTask, TestContext.Current.CancellationToken);
        var activeTasks = (await _repository.GetActiveTasksAsync(TestContext.Current.CancellationToken)).ToList();

        Assert.Contains(activeTasks, t => t.Id == lowPriorityTask.Id && t.Priority == Priority.Low);
        Assert.Contains(activeTasks, t => t.Id == highPriorityTask.Id && t.Priority == Priority.High);
    }

    [Fact]
    public async System.Threading.Tasks.Task SaveChangesAsync_AfterUpdatingPriority_PersistsUpdatedPriority()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");
        await _repository.AddTaskAsync(task, TestContext.Current.CancellationToken);

        task.UpdatePriority(Priority.High);
        await _repository.SaveChangesAsync(task, TestContext.Current.CancellationToken);
        var persisted = await _repository.FindByIdAsync(task.Id, TestContext.Current.CancellationToken);

        Assert.NotNull(persisted);
        Assert.Equal(Priority.High, persisted.Priority);
    }
}
