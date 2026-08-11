using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.Api;
using TaskManager.IntegrationTests.Tasks;
using TaskStatus = TaskManager.Domain.Tasks.TaskStatus;

namespace TaskManager.IntegrationTests;

/// <summary>
/// Integration tests for Task Manager API - Lab 4
/// </summary>
public sealed class TaskApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TaskApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateTask_WithValidData_ShouldReturn201()
    {
        var response = await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<TaskResponseDto>();
        Assert.NotNull(body);
        Assert.Equal("Finish report", body!.Title);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTask_WithExistingId_ShouldReturn200()
    {
        var created = await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report" });
        var createdTask = await created.Content.ReadFromJsonAsync<TaskResponseDto>();

        var response = await _client.GetAsync($"/tasks/{createdTask!.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTask_WithNonExistentId_ShouldReturn404()
    {
        var response = await _client.GetAsync($"/tasks/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateTaskStatus_WithValidData_ShouldReturn200()
    {
        var created = await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report" });
        var createdTask = await created.Content.ReadFromJsonAsync<TaskResponseDto>();

        var response = await _client.PutAsJsonAsync($"/tasks/{createdTask!.Id}/status", new { Status = (int)TaskStatus.InProgress });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<TaskResponseDto>();
        Assert.Equal((int)TaskStatus.InProgress, body!.Status);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetActiveTasks_ShouldReturn200WithTaskList()
    {
        await _client.PostAsJsonAsync("/tasks", new { Title = "Finish report", Description = "Complete the quarterly report" });

        var response = await _client.GetAsync("/tasks");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var tasks = await response.Content.ReadFromJsonAsync<List<TaskResponseDto>>();
        Assert.NotNull(tasks);
        Assert.NotEmpty(tasks!);
    }
}

