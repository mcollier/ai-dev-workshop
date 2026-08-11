using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.Api;
using Priority = TaskManager.Domain.Tasks.Priority;

namespace TaskManager.IntegrationTests.Tasks;

public sealed class UpdateTaskPriorityEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UpdateTaskPriorityEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PutTasksPriority_WithExistingTask_ReturnsOkWithUpdatedPriority()
    {
        var created = await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report" });
        var createdTask = await created.Content.ReadFromJsonAsync<TaskResponseDto>();

        var response = await _client.PutAsJsonAsync($"/tasks/{createdTask!.Id}/priority", new { Priority = (int)Priority.High });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<TaskResponseDto>();
        Assert.Equal((int)Priority.High, body!.Priority);
    }

    [Fact]
    public async Task PutTasksPriority_WithNonExistentTask_ReturnsNotFound()
    {
        var response = await _client.PutAsJsonAsync($"/tasks/{Guid.NewGuid()}/priority", new { Priority = (int)Priority.High });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PutTasksPriority_WithInvalidPriorityValue_ReturnsBadRequest()
    {
        var created = await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report" });
        var createdTask = await created.Content.ReadFromJsonAsync<TaskResponseDto>();

        var response = await _client.PutAsJsonAsync($"/tasks/{createdTask!.Id}/priority", new { Priority = 99 });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
