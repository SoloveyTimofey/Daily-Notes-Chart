using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

public sealed class ChartGroupName : ValueObject
{
    public const int NAME_MIN_LENGHT = 3;
    public const int NAME_MAX_LENGHT = 50;

    #pragma warning disable
    private ChartGroupName() { }
    #pragma warning enable
    private ChartGroupName(string name) => Value = name;

    public string Value { get; }

    public static Result<ChartGroupName> Create(string name)
    {
        if (name.Length < NAME_MIN_LENGHT || name.Length > NAME_MAX_LENGHT)
        {
            return Result.Failure<ChartGroupName>(DomainErrors.ChartGroup.InvalidChartGroupName);
        }

        return Result.Success(new ChartGroupName(name));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}