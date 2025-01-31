using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;

public sealed class TimeOnlyNote : NoteBase
{
    #pragma warning disable
    private TimeOnlyNote() { }
    #pragma warning enable
    private TimeOnlyNote(
        ChartId chartId,
        TimeOnly time,
        Color color,
        NoteDescription description) : base(chartId, time, color, description)
    {
    }

    public static Result<TimeOnlyNote> Create(ChartId chartId, TimeOnly time, Color color, NoteDescription description) =>
        Result.Success(
            new TimeOnlyNote(
                chartId,
                time,
                color,
                description
            )
        );

    public static Result<TimeOnlyNote> CreateTemplateBased(ChartId chartId, TimeOnly time, NoteTemplate template)
    {
        return Result.Success(
            new TimeOnlyNote(
                chartId,
                time,
                template.Color,
                template.Description
            )
        );
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return ChartId.Id;
        yield return Time;
        yield return Color.Value;
        yield return Description;
    }
}