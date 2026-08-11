using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.Api;
using Priority = TaskManager.Domain.Tasks.Priority;

namespace TaskManager.IntegrationTests.Tasks;

public sealed class CreateTaskEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CreateTaskEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostTasks_WithoutPriority_ReturnsCreatedWithMediumPriority()
    {
        var response = await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<TaskResponseDto>();
        Assert.NotNull(body);
        Assert.Equal((int)Priority.Medium, body!.Priority);
    }

    [Fact]
    public async Task PostTasks_WithExplicitPriority_ReturnsCreatedWithThatPriority()
    {
        var response = await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report", Priority = (int)Priority.High });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<TaskResponseDto>();
        Assert.NotNull(body);
        Assert.Equal((int)Priority.High, body!.Priority);
    }
}
