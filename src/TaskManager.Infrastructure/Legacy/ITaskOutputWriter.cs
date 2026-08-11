namespace TaskManager.Infrastructure.Legacy;

/// <summary>
/// Persists processed task output, decoupled from the processing logic
/// </summary>
public interface ITaskOutputWriter
{
    Task WriteAsync(string fileName, string content, CancellationToken cancellationToken = default);
}
