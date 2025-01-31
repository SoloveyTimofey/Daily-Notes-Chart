using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

public sealed class ChartSummary : ValueObject
{
    public const int SUMMARY_MIN_LENGHT = 0;
    public const int SUMMARY_MAX_LENGHT = 500;

    #pragma warning disable
    private ChartSummary() { }
    #pragma warning enable
    private ChartSummary(string summary) => Value = summary;

    public string Value { get; }

    public static Result<ChartSummary> Create(string summary)
    {
        if (summary.Length < SUMMARY_MIN_LENGHT || summary.Length > SUMMARY_MAX_LENGHT)
        {
            return Result.Failure<ChartSummary>(DomainErrors.Chart.InvalidChartSummary);
        }

        return Result.Success(new ChartSummary(summary));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}