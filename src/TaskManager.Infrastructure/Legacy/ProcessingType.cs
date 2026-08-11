namespace TaskManager.Infrastructure.Legacy;

/// <summary>
/// Identifies which text-processing algorithm <see cref="LegacyTaskProcessor"/> should apply
/// </summary>
public enum ProcessingType
{
    CaseInversion = 1,
    SentenceCase = 2
}
