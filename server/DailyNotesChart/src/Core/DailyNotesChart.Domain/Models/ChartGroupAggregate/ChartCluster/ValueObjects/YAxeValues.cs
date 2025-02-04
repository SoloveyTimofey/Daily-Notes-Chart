using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

public class YAxeValues : ValueObject
{
    public const int MIN_VALUE = -10000;
    public const int MAX_VALUE = 10000;

    //private YAxeValues() { }
    public YAxeValues() { } // For JsonConverter
    private YAxeValues(double start, double end, bool isInteger)
    {
        Start = start;
        End = end;
        IsInteger = isInteger;
    }
    public double Start { get; }
    public double End { get; }
    public bool IsInteger { get; }

    public static Result<YAxeValues> Create(double start, double end, bool isInteger)
    {
        if (start > end)
        {
            return Result.Failure<YAxeValues>(DomainErrors.Chart.StartValueGreaterThanEndValue);
        }

        if (start < MIN_VALUE || end > MAX_VALUE)
        {
            return Result.Failure<YAxeValues>(DomainErrors.Chart.ValuesOutOfRange);
        }

        if (isInteger is true)
        {
            start = (int)start;
            end = (int)end;
        }

        return Result.Success(new YAxeValues(start, end, isInteger));
    }

    public static YAxeValues CreateDefault() => new YAxeValues(start: 0, end: 10, isInteger: true);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Start;
        yield return End;
    }
}