using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;

namespace TaskManager.UnitTests.Tasks.TaskTests;

public sealed class UpdatePriorityTests
{
    [Theory]
    [InlineData(Priority.Low)]
    [InlineData(Priority.Medium)]
    [InlineData(Priority.High)]
    public void UpdatePriority_WithNewPriority_UpdatesPriority(Priority newPriority)
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");

        task.UpdatePriority(newPriority);

        Assert.Equal(newPriority, task.Priority);
    }

    [Fact]
    public void UpdatePriority_WhenCalled_UpdatesTimestamp()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");
        var originalUpdatedAt = task.UpdatedAt;

        task.UpdatePriority(Priority.High);

        Assert.True(task.UpdatedAt >= originalUpdatedAt);
    }
}
