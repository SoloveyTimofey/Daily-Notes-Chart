using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster;

public sealed class TimeOnlyChart : ChartBase
{
    private TimeOnlyChart(
        ChartId id,
        ChartSummary summary,
        ChartDate date,
        ChartGroupId chartGroupId) : base(id, summary, date, chartGroupId)
    {
    }

    public static Result<TimeOnlyChart> Create(ChartSummary summary, ChartDate date, ChartGroupId chartGroupId) =>
        Result.Success(
            new TimeOnlyChart(
                id: new ChartId(Guid.NewGuid()),
                summary,
                date,
                chartGroupId
            )
        );

    protected override bool CheckIfChartDoesNotContainDuplicateNote(NoteBase noteToAdd)
    {
        if (Notes.Any(existingNote => existingNote.Time == noteToAdd.Time))
        {
            return false;
        }

        return true;
    }

    protected override (bool isValid, SpecifiedInvalidNoteTypeForChartException? exception) CheckIfNoteTypeValid(NoteBase note)
    {
        if (note is not TimeOnlyNote)
        {
            var exception = new SpecifiedInvalidNoteTypeForChartException(
                chartType: typeof(TimeOnlyChart),
                requiredNoteType: typeof(TimeOnlyNote)
            );

            return (isValid: false, exception);
        }

        return (isValid: true, exception: null);
    }
}