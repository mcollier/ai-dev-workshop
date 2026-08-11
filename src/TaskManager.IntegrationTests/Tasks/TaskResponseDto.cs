namespace TaskManager.IntegrationTests.Tasks;

/// <summary>
/// Shape used to deserialize task API responses in integration tests
/// </summary>
public sealed record TaskResponseDto(Guid Id, string Title, string Description, int Status, int Priority, DateTime CreatedAt, DateTime UpdatedAt, DateOnly? DueDate = null);
