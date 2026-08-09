using TaskManager.Domain.Tasks;

namespace TaskManager.UnitTests.TestData;

/// <summary>
/// Builds realistic Task instances for tests. Extend the value pools below
/// rather than hardcoding single-use literals in test methods.
/// </summary>
public static class TaskFactory
{
    private static readonly (string Title, string Description)[] Samples =
    [
        ("Fix login redirect bug", "Users land on a 404 after SSO callback."),
        ("Write API docs for /tasks endpoint", "Document request/response shapes and error codes."),
        ("Add pagination to task list", "Support page/pageSize query parameters."),
        ("Investigate flaky integration test", "TaskRepositoryTests times out intermittently in CI."),
        ("Upgrade FakeItEasy to latest", "Check for breaking changes in mock setup syntax."),
    ];

    /// <summary>Creates a single valid Task with default status Todo.</summary>
    public static Task CreateOne(int index = 0)
    {
        var (title, description) = Samples[index % Samples.Length];
        return Task.Create(title, description);
    }

    /// <summary>Creates <paramref name="count"/> tasks, cycling through TaskStatus values.</summary>
    public static IReadOnlyList<Task> CreateMany(int count)
    {
        var statuses = Enum.GetValues<TaskStatus>();
        var tasks = new List<Task>(count);

        for (var i = 0; i < count; i++)
        {
            var task = CreateOne(i);
            task.UpdateStatus(statuses[i % statuses.Length]);
            tasks.Add(task);
        }

        return tasks;
    }

    /// <summary>Creates a task with an intentionally long title, for boundary tests.</summary>
    public static Task CreateWithMaxLengthTitle(int length = 200) =>
        Task.Create(new string('T', length), "Boundary test: max-length title.");

    /// <summary>Creates a task that has been updated after creation, so CreatedAt != UpdatedAt.</summary>
    public static Task CreateUpdated()
    {
        var task = CreateOne();
        task.UpdateDetails(task.Title, "Updated description after initial creation.");
        return task;
    }
}
