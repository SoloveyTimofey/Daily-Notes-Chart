namespace DailyNotesChart.Application.ReadModels;

public sealed class ChartGroupReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;

    public Guid? DefaultNoteTemplateId { get; init; } = null!;
    public NoteTemplateReadModel? DefaultNoteTemplate { get; init; } = null!;
    public Guid CreatorId { get; init; }

    public DefaultChartTemplateReadModel DefaultChartTemplate { get; init; } = null!;

    public List<ChartBaseReadModel> Charts { get; init; } = [];
    public List<NoteTemplateReadModel> NoteTemplates { get; init; } = [];
}