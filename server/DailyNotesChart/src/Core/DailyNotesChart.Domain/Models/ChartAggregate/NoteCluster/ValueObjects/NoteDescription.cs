using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;

public class NoteDescription : ValueObject
{
    public const int DESCRIPTION_MIN_LENGHT = 0;
    public const int DESCRIPTION_MAX_LENGHT = 300;

    private NoteDescription() { }
    private NoteDescription(string? description) => Value = description;

    public string? Value { get; }

    public static Result<NoteDescription> Create(string? description)
    {
        if(string.IsNullOrEmpty(description)) return Result.Success(new NoteDescription(description));

        if (description.Length < DESCRIPTION_MIN_LENGHT || description.Length > DESCRIPTION_MAX_LENGHT)
        {
            return Result.Failure<NoteDescription>(DomainErrors.NoteTemplate.InvalidDescription);
        }

        return Result.Success(new NoteDescription(description));
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }
}