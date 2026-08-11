namespace TaskManager.Infrastructure.Legacy;

/// <summary>
/// Writes processed task output to the local file system
/// </summary>
public sealed class FileTaskOutputWriter : ITaskOutputWriter
{
    public Task WriteAsync(string fileName, string content, CancellationToken cancellationToken = default) =>
        File.WriteAllTextAsync(fileName, content, cancellationToken);
}
