using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

public sealed class ChartDate : ValueObject
{
    public static readonly DateOnly MIN_DATE = new(1900, 1, 1);
    public static readonly DateOnly MAX_DATE = new(2300, 1, 1);

    private ChartDate(DateOnly date) => Value = date;

    public DateOnly Value { get; }

    public static Result<ChartDate> Create(DateOnly date)
    {
        if (date < MIN_DATE || date > MAX_DATE)
        {
            return Result.Failure<ChartDate>(DomainErrors.Chart.InvaildChartDate);
        }

        return Result.Success(new ChartDate(date));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}