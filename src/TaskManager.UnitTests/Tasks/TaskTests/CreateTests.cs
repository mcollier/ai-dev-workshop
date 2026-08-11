using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;

namespace TaskManager.UnitTests.Tasks.TaskTests;

public sealed class CreateTests
{
    [Fact]
    public void Create_WithoutPriority_DefaultsToMedium()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");

        Assert.Equal(Priority.Medium, task.Priority);
    }

    [Theory]
    [InlineData(Priority.Low)]
    [InlineData(Priority.Medium)]
    [InlineData(Priority.High)]
    public void Create_WithExplicitPriority_SetsPriority(Priority priority)
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report", priority);

        Assert.Equal(priority, task.Priority);
    }
}
