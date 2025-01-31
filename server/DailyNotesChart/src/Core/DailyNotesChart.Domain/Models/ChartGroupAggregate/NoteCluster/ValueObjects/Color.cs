using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared;
using System.Text.RegularExpressions;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;

public sealed class Color : ValueObject
{
    #pragma warning disable
    private Color() { }
    #pragma warning enable
    private Color(string color) => Value = color;

    public string Value { get; }

    public static Result<Color> Create(string color)
    {
        Regex hexColorRegex = new Regex("^#([A-Fa-f0-9]{6})$");

        if (hexColorRegex.IsMatch(color) is false)
        {
            return Result.Failure<Color>(DomainErrors.NoteTemplate.InvalidColorFormat);
        }

        return Result.Success(new Color(color));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}