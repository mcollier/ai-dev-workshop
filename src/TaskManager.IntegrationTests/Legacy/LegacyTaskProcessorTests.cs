using Microsoft.Extensions.Logging.Abstractions;
using TaskManager.Infrastructure.Legacy;

namespace TaskManager.IntegrationTests.Legacy;

public sealed class LegacyTaskProcessorTests
{
    private readonly RecordingTaskOutputWriter _outputWriter = new();
    private readonly LegacyTaskProcessor _processor;

    public LegacyTaskProcessorTests()
    {
        _processor = new LegacyTaskProcessor(NullLogger<LegacyTaskProcessor>.Instance, _outputWriter);
    }

    [Fact]
    public async Task ProcessTaskAsync_WithNullOrEmptyInput_ReturnsEmptyString()
    {
        var result = await _processor.ProcessTaskAsync(1, string.Empty, ProcessingType.CaseInversion, shouldInvertCase: true, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task ProcessTaskAsync_CaseInversionWithInvert_InvertsCaseReplacesSpacesAndPersists()
    {
        var result = await _processor.ProcessTaskAsync(42, "Hello World", ProcessingType.CaseInversion, shouldInvertCase: true, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal("hELLO_wORLD", result);
        Assert.Equal(("task_42.txt", "hELLO_wORLD"), _outputWriter.LastWrite);
    }

    [Fact]
    public async Task ProcessTaskAsync_CaseInversionWithInvert_TruncatesResultAt50Characters()
    {
        var longInput = new string('a', 60);

        var result = await _processor.ProcessTaskAsync(1, longInput, ProcessingType.CaseInversion, shouldInvertCase: true, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(50, result.Length);
    }

    [Fact]
    public async Task ProcessTaskAsync_CaseInversionWithoutInvert_ReturnsUpperCaseWithoutPersisting()
    {
        var result = await _processor.ProcessTaskAsync(1, "Hello World", ProcessingType.CaseInversion, shouldInvertCase: false, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal("HELLO WORLD", result);
        Assert.Null(_outputWriter.LastWrite);
    }

    [Fact]
    public async Task ProcessTaskAsync_SentenceCase_KeepsFirstWordAndLowersTheRest()
    {
        var result = await _processor.ProcessTaskAsync(1, "HELLO WORLD AGAIN", ProcessingType.SentenceCase, shouldInvertCase: false, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal("HELLO world again", result);
    }

    [Fact]
    public async Task ProcessTaskAsync_WithUnknownProcessingType_ReturnsInputUnchanged()
    {
        var result = await _processor.ProcessTaskAsync(1, "Untouched", (ProcessingType)99, shouldInvertCase: false, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal("Untouched", result);
    }

    [Fact]
    public async Task ProcessTaskAsync_WhenOutputWriterThrows_DoesNotPropagateException()
    {
        var processor = new LegacyTaskProcessor(NullLogger<LegacyTaskProcessor>.Instance, new ThrowingTaskOutputWriter());

        var result = await processor.ProcessTaskAsync(1, "Hello World", ProcessingType.CaseInversion, shouldInvertCase: true, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal("hELLO_wORLD", result);
    }

    [Fact]
    public async Task ProcessTaskAsync_WithoutOutputWriter_DoesNotThrow()
    {
        var processor = new LegacyTaskProcessor(NullLogger<LegacyTaskProcessor>.Instance);

        var result = await processor.ProcessTaskAsync(1, "Hello World", ProcessingType.CaseInversion, shouldInvertCase: true, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal("hELLO_wORLD", result);
    }

    private sealed class RecordingTaskOutputWriter : ITaskOutputWriter
    {
        public (string FileName, string Content)? LastWrite { get; private set; }

        public Task WriteAsync(string fileName, string content, CancellationToken cancellationToken = default)
        {
            LastWrite = (fileName, content);
            return Task.CompletedTask;
        }
    }

    private sealed class ThrowingTaskOutputWriter : ITaskOutputWriter
    {
        public Task WriteAsync(string fileName, string content, CancellationToken cancellationToken = default) =>
            throw new IOException("Simulated write failure");
    }
}
