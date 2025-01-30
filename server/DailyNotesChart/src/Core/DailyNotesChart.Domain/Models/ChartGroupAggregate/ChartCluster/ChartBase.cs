using DailyNotesChart.Domain.Errors;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster;

public abstract class ChartBase : Entity<ChartId>
{
    private List<NoteBase> _notes = new();
    protected ChartBase(
        ChartId id,
        ChartSummary? summary,
        ChartDate date,
        ChartGroupId chartGroupId) : base(id)
    {
        Date = date;
        Summary = summary;
        ChartGroupId = chartGroupId;
    }

    public ChartDate Date { get; protected set; }
    public ChartSummary? Summary { get; protected set; }
    public IReadOnlyCollection<NoteBase> Notes => _notes;
    public ChartGroupId ChartGroupId { get; protected set; }

    // Pattern: Template Method
    /// <exception cref="SpecifiedInvalidNoteTypeForChartException"/>
    internal Result AddNote(NoteBase noteToAdd)
    {
        var checkIfNoteTypeValidResult = CheckIfNoteTypeValid(noteToAdd);

        if (checkIfNoteTypeValidResult.isValid == false)
        {
            throw checkIfNoteTypeValidResult.exception!;
        }

        // Bussiness rule: cannot add note if note with the same coordinates already exists on current chart
        if (CheckIfChartDoesNotContainDuplicateNote(noteToAdd) is false)
        {
            return Result.Failure(DomainErrors.Chart.CannotAddNoteWithDuplicateCoordinates);
        }

        _notes.Add(noteToAdd);

        return Result.Success();
    }

    protected abstract (bool isValid, SpecifiedInvalidNoteTypeForChartException? exception) CheckIfNoteTypeValid(NoteBase note);
    protected abstract bool CheckIfChartDoesNotContainDuplicateNote(NoteBase noteToAdd);
}