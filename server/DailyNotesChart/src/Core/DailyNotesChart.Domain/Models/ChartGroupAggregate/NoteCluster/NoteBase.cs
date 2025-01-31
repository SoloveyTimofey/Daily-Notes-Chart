using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Primitives;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster;

public abstract class NoteBase : ValueObject
{
    #pragma warning disable
    protected NoteBase() { }
    #pragma warning enable
    protected NoteBase(
        ChartId chartId,
        TimeOnly time,
        Color color,
        NoteDescription description)
    {
        ChartId = chartId;
        Time = time;
        Color = color;
        Description = description;
    }

    public ChartId ChartId { get; protected set; }
    public TimeOnly Time { get; protected set; }

    public Color Color { get; protected set; }
    public NoteDescription Description { get; protected set; }
}
