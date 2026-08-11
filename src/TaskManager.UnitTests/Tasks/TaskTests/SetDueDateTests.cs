using TaskManager.Domain.Tasks;
using DomainTask = TaskManager.Domain.Tasks.Task;

namespace TaskManager.UnitTests.Tasks.TaskTests;

public sealed class SetDueDateTests
{
    [Fact]
    public void SetDueDate_WithFutureDate_SetsDueDate()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");
        var futureDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7));

        task.SetDueDate(futureDate);

        Assert.Equal(futureDate, task.DueDate);
    }

    [Fact]
    public void SetDueDate_WithNull_ClearsDueDate()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report", dueDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)));

        task.SetDueDate(null);

        Assert.Null(task.DueDate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void SetDueDate_WithTodayOrPastDate_ThrowsArgumentException(int daysOffset)
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");
        var invalidDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(daysOffset));

        Assert.Throws<ArgumentException>(() => task.SetDueDate(invalidDate));
    }

    [Fact]
    public void SetDueDate_WhenCalled_UpdatesTimestamp()
    {
        var task = DomainTask.Create("Finish report", "Complete the quarterly report");
        var originalUpdatedAt = task.UpdatedAt;

        task.SetDueDate(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)));

        Assert.True(task.UpdatedAt >= originalUpdatedAt);
    }
}
