using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;

public sealed class NoteTemplate : Entity<NoteTemplateId>
{
    #pragma warning disable
    private NoteTemplate() { }
    #pragma warning enable
    private NoteTemplate(
        NoteTemplateId id,
        ChartGroupId chartGroupId,
        Color color,
        NoteDescription description
        ) : base(id)
    {
        ChartGroupId = chartGroupId;
        Color = color;
        Description = description;
    }

    public ChartGroupId ChartGroupId { get; }
    public Color Color { get; }
    public NoteDescription Description { get; }

    public static Result<NoteTemplate> Create(ChartGroupId chartGroupId, Color color, NoteDescription description) =>
        Result.Success(
            new NoteTemplate(
                id: new NoteTemplateId(Guid.NewGuid()),
                chartGroupId,
                color,
                description
            )
        );

    public void Deconstruct(out Color color, out NoteDescription? description)
    {
        color = Color;
        description = Description;
    }
}