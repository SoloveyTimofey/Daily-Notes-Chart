using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;

public sealed class TwoDimentionalNote : NoteBase
{
    #pragma warning disable
    private TwoDimentionalNote() { } // EF Core constructor
    #pragma warning disable
    private TwoDimentionalNote(
        ChartId chartId,
        TimeOnly time,
        Color color,
        NoteDescription description,
        double yAxeValue) : base(chartId, time, color, description)
    {
        YAxeValue = yAxeValue;
    }
    public double YAxeValue { get; private set; }

    /// <exception cref="SpecifiedYAxeValuOutOfRangeException"></exception>
    public static Result<TwoDimentionalNote> Create(ChartId chartId, TimeOnly time, Color color, NoteDescription description, double yAxeValue, TwoDimentionalChart chart)
    {
        bool isYAxeValueValid = ValidateYAxeValue(yAxeValue, chartId, chart);

        if (isYAxeValueValid is false)
        {
            throw new SpecifiedYAxeValuOutOfRangeException("Cannot create note. Specified Y axe value is out of chart's range.");
        }

        return Result.Success(
            new TwoDimentionalNote(
                chartId,
                time,
                color,
                description,
                yAxeValue
            )
        );
    }

    /// <exception cref="SpecifiedYAxeValuOutOfRangeException"></exception>
    public static Result<TwoDimentionalNote> CreateTemplateBased(ChartId chartId, TimeOnly time, double yAxeValue, TwoDimentionalChart chart, NoteTemplate template)
    {
        bool isYAxeValueValid = ValidateYAxeValue(yAxeValue, chartId, chart);

        if (isYAxeValueValid is false)
        {
            throw new SpecifiedYAxeValuOutOfRangeException("Cannot create note. Specified Y axe value is out of chart's range.");
        }

        return Result.Success(
            new TwoDimentionalNote(
                chartId,
                time,
                template.Color,
                template.Description,
                yAxeValue
            )
        );
    }

    private static bool ValidateYAxeValue(double yAxeValue, ChartId chartId, TwoDimentionalChart chart)
    {
        YAxeValues yValues = chart.YAxeValues;

        if (yAxeValue < yValues.Start || yAxeValue > yValues.End)
        {
            return false;
        }

        return true;
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return ChartId.Id;
        yield return Time;
        yield return Color.Value;
        yield return Description;
        yield return YAxeValue;
    }
}