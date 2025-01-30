using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster;

public sealed class TwoDimentionalChart : ChartBase
{
    private TwoDimentionalChart(
        ChartId id,
        ChartSummary? summary,
        ChartDate date,
        ChartGroupId chartGroupId,
        YAxeValues yAxeValues,
        YAxeName yAxeName) : base(id, summary, date, chartGroupId)
    {
        YAxeValues = yAxeValues;
        YAxeName = yAxeName;
    }

    public YAxeValues YAxeValues { get; private set; }
    public YAxeName YAxeName { get; private set; }

    public static Result<TwoDimentionalChart> Create(ChartSummary? summary, ChartDate date, ChartGroupId chartGroupId, YAxeValues yAxeValues, YAxeName yAxeName) =>
        Result.Success(
            new TwoDimentionalChart(
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
        var twoDimensionalNoteToAdd = (TwoDimentionalNote)noteToAdd;

        if (Notes.Cast<TwoDimentionalNote>().Any(existingNote =>
            existingNote.Time == twoDimensionalNoteToAdd.Time ||
            twoDimensionalNoteToAdd.YAxeValue == existingNote.YAxeValue))
        {
            return false;
        }

        return true;
    }

    protected override (bool isValid, SpecifiedInvalidNoteTypeForChartException? exception) CheckIfNoteTypeValid(NoteBase note)
    {
        if (note is not TwoDimentionalNote)
        {
            var exception = new SpecifiedInvalidNoteTypeForChartException(
                chartType: typeof(TwoDimentionalChart),
                requiredNoteType: typeof(TwoDimentionalNote)
            );

            return (isValid: false, exception);
        }

        return (isValid: true, exception: null);
    }
}