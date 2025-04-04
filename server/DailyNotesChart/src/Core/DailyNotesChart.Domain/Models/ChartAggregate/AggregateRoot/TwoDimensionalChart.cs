using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;

public sealed class TwoDimensionalChart : ChartBase
{
    private TwoDimensionalChart(
        ChartId id,
        ChartSummary summary,
        ChartDate date,
        ChartGroupId chartGroupId,
        YAxeValues yAxeValues,
        YAxeName yAxeName) : base(id, summary, date, chartGroupId)
    {
        YAxeValues = yAxeValues;
        YAxeName = yAxeName;
    }



    #region For EF Core
    // Return to this
    #pragma warning disable
    private TwoDimensionalChart(
        ChartId id,
        ChartSummary summary,
        ChartDate date,
        ChartGroupId chartGroupId) : base(id, summary, date, chartGroupId)
    {
    }
    #pragma warning enable
    // Method for EF Core
    private static Result<TwoDimensionalChart> CreateEFCore(ChartSummary summary, ChartDate date, ChartGroupId chartGroupId, YAxeValues yAxeValues, YAxeName yAxeName) =>
        Result.Success(
            new TwoDimensionalChart(
                id: new ChartId(Guid.NewGuid()),
                summary,
                date,
                chartGroupId
            )
            {
                YAxeValues = yAxeValues,
                YAxeName = yAxeName
            }
        );
    #endregion

    public YAxeValues YAxeValues { get; private set; }
    public YAxeName YAxeName { get; private set; }

    internal static Result<TwoDimensionalChart> Create(ChartSummary summary, ChartDate date, ChartGroupId chartGroupId, YAxeValues yAxeValues, YAxeName yAxeName) =>
        Result.Success(
            new TwoDimensionalChart(
                id: new ChartId(Guid.NewGuid()),
                summary,
                date,
                chartGroupId,
                yAxeValues,
                yAxeName
            )
        );

    protected override bool CheckIfChartDoesNotContainDuplicateNote(NoteBase noteToAdd)
    {
        var twoDimensionalNoteToAdd = (TwoDimensionalNote)noteToAdd;

        if (Notes.Cast<TwoDimensionalNote>().Any(existingNote =>
            existingNote.Time == twoDimensionalNoteToAdd.Time ||
            twoDimensionalNoteToAdd.YAxeValue == existingNote.YAxeValue))
        {
            return false;
        }

        return true;
    }

    protected override (bool isValid, SpecifiedInvalidNoteTypeForChartException? exception) CheckIfNoteTypeValid(NoteBase note)
    {
        if (note is not TwoDimensionalNote)
        {
            var exception = new SpecifiedInvalidNoteTypeForChartException(
                chartType: typeof(TwoDimensionalChart),
                requiredNoteType: typeof(TwoDimensionalNote)
            );

            return (isValid: false, exception);
        }

        return (isValid: true, exception: null);
    }
}