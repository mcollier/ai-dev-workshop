using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.Api;
using Priority = TaskManager.Domain.Tasks.Priority;

namespace TaskManager.IntegrationTests.Tasks;

public sealed class GetTasksEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GetTasksEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTasks_WithoutPriorityFilter_ReturnsAllTasks()
    {
        await _client.PostAsJsonAsync("/tasks", new { Title = "A", Description = "A desc", Priority = (int)Priority.Low }, TestContext.Current.CancellationToken);
        await _client.PostAsJsonAsync("/tasks", new { Title = "B", Description = "B desc", Priority = (int)Priority.High }, TestContext.Current.CancellationToken);

        var response = await _client.GetAsync("/tasks", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var tasks = await response.Content.ReadFromJsonAsync<List<TaskResponseDto>>(TestContext.Current.CancellationToken);
        Assert.NotNull(tasks);
        Assert.True(tasks!.Count >= 2);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTasks_FilteredByPriority_ReturnsOnlyMatchingTasks()
    {
        await _client.PostAsJsonAsync("/tasks", new { Title = "Filter-Low", Description = "desc", Priority = (int)Priority.Low }, TestContext.Current.CancellationToken);
        await _client.PostAsJsonAsync("/tasks", new { Title = "Filter-High", Description = "desc", Priority = (int)Priority.High }, TestContext.Current.CancellationToken);

        var response = await _client.GetAsync("/tasks?priority=High", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var tasks = await response.Content.ReadFromJsonAsync<List<TaskResponseDto>>(TestContext.Current.CancellationToken);
        Assert.NotNull(tasks);
        Assert.Contains(tasks!, t => t.Title == "Filter-High");
        Assert.DoesNotContain(tasks!, t => t.Title == "Filter-Low");
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTasks_WithInvalidPriorityFilter_ReturnsBadRequest()
    {
        var response = await _client.GetAsync("/tasks?priority=NotAPriority", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTasks_SortedByPriorityDefault_ReturnsHighestFirst()
    {
        await _client.PostAsJsonAsync("/tasks", new { Title = "Sort-Low", Description = "desc", Priority = (int)Priority.Low }, TestContext.Current.CancellationToken);
        await _client.PostAsJsonAsync("/tasks", new { Title = "Sort-High", Description = "desc", Priority = (int)Priority.High }, TestContext.Current.CancellationToken);

        var response = await _client.GetAsync("/tasks?sortBy=priority", TestContext.Current.CancellationToken);
        var tasks = await response.Content.ReadFromJsonAsync<List<TaskResponseDto>>(TestContext.Current.CancellationToken);

        var sortTasks = tasks!.Where(t => t.Title.StartsWith("Sort-")).ToList();
        Assert.True(sortTasks[0].Priority >= sortTasks[^1].Priority);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTasks_SortedByPriorityDescOrder_ReturnsLowestFirst()
    {
        await _client.PostAsJsonAsync("/tasks", new { Title = "SortDesc-Low", Description = "desc", Priority = (int)Priority.Low }, TestContext.Current.CancellationToken);
        await _client.PostAsJsonAsync("/tasks", new { Title = "SortDesc-High", Description = "desc", Priority = (int)Priority.High }, TestContext.Current.CancellationToken);

        var response = await _client.GetAsync("/tasks?sortBy=priority&sortOrder=desc", TestContext.Current.CancellationToken);
        var tasks = await response.Content.ReadFromJsonAsync<List<TaskResponseDto>>(TestContext.Current.CancellationToken);

        var sortTasks = tasks!.Where(t => t.Title.StartsWith("SortDesc-")).ToList();
        Assert.True(sortTasks[0].Priority <= sortTasks[^1].Priority);
    }
}
