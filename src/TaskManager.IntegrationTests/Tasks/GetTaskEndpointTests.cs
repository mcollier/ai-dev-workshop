using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.Api;
using Priority = TaskManager.Domain.Tasks.Priority;

namespace TaskManager.IntegrationTests.Tasks;

public sealed class GetTaskEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GetTaskEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTaskById_WithExistingTask_ReturnsOkWithPriority()
    {
        var created = await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report", Priority = (int)Priority.Low }, TestContext.Current.CancellationToken);
        var createdTask = await created.Content.ReadFromJsonAsync<TaskResponseDto>(TestContext.Current.CancellationToken);

        var response = await _client.GetAsync($"/tasks/{createdTask!.Id}", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<TaskResponseDto>(TestContext.Current.CancellationToken);
        Assert.Equal((int)Priority.Low, body!.Priority);
    }

    [Fact]
    public async Task GetTaskById_WithNonExistentTask_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/tasks/{Guid.NewGuid()}", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
