using System.Text;
using Microsoft.Extensions.Logging;

namespace TaskManager.Infrastructure.Legacy;

/// <summary>
/// Refactored during Lab 3: async, guard clauses, structured logging, and single-responsibility methods
/// </summary>
public sealed class LegacyTaskProcessor
{
    private const int MaxResultLength = 50;
    private const int SimulatedProcessingDelayMilliseconds = 100;

    private readonly ILogger<LegacyTaskProcessor> _logger;
    private readonly ITaskOutputWriter? _outputWriter;

    public LegacyTaskProcessor(ILogger<LegacyTaskProcessor> logger, ITaskOutputWriter? outputWriter = null)
    {
        _logger = logger;
        _outputWriter = outputWriter;
    }

    public async Task<string> ProcessTaskAsync(int taskId, string? inputText, ProcessingType processingType, bool shouldInvertCase, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(inputText))
        {
            _logger.LogWarning("Task {TaskId} skipped: input text is null or empty", taskId);
            return string.Empty;
        }

        _logger.LogInformation("Processing task {TaskId} with {ProcessingType}", taskId, processingType);

        return processingType switch
        {
            ProcessingType.CaseInversion when shouldInvertCase => await InvertCaseAndPersistAsync(taskId, inputText, cancellationToken),
            ProcessingType.CaseInversion => inputText.ToUpperInvariant(),
            ProcessingType.SentenceCase => ToSentenceCase(inputText),
            _ => inputText
        };
    }

    private async Task<string> InvertCaseAndPersistAsync(int taskId, string inputText, CancellationToken cancellationToken)
    {
        var inverted = InvertCaseAndReplaceSpaces(inputText);
        var truncated = TruncateIfNeeded(inverted, MaxResultLength);

        await Task.Delay(SimulatedProcessingDelayMilliseconds, cancellationToken);
        await PersistResultAsync(taskId, truncated, cancellationToken);

        return truncated;
    }

    private static string InvertCaseAndReplaceSpaces(string inputText)
    {
        var builder = new StringBuilder(inputText.Length);

        foreach (var character in inputText)
            builder.Append(ToInvertedCaseOrUnderscore(character));

        return builder.ToString();
    }

    private static char ToInvertedCaseOrUnderscore(char character)
    {
        if (character == ' ')
            return '_';

        return char.IsUpper(character) ? char.ToLowerInvariant(character) : char.ToUpperInvariant(character);
    }

    private static string TruncateIfNeeded(string inputText, int maxLength) =>
        inputText.Length > maxLength ? inputText[..maxLength] : inputText;

    private static string ToSentenceCase(string inputText)
    {
        var words = inputText.Split(' ');
        var builder = new StringBuilder(words[0]);

        for (var wordIndex = 1; wordIndex < words.Length; wordIndex++)
            builder.Append(' ').Append(words[wordIndex].ToLowerInvariant());

        return builder.ToString();
    }

    private async Task PersistResultAsync(int taskId, string content, CancellationToken cancellationToken)
    {
        if (_outputWriter is null)
        {
            _logger.LogDebug("No output writer configured; skipping persistence for task {TaskId}", taskId);
            return;
        }

        try
        {
            await _outputWriter.WriteAsync($"task_{taskId}.txt", content, cancellationToken);
        }
        catch (IOException exception)
        {
            _logger.LogError(exception, "Failed to persist output for task {TaskId}", taskId);
        }
    }
}

