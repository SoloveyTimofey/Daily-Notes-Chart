namespace DailyNotesChart.Application.ReadModels;

public class ChartGroupReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid DefaultNoteTemplateId { get; set; }
    public NoteTemplateReadModel? DefaultNoteTemplate { get; set; }
    public DefaultChartTemplateReadModel DefaultChartTemplate { get; set; } = null!;

    public ICollection<ChartBaseReadModel> Charts { get; set; } = [];
    public ICollection<NoteTemplateReadModel> NoteTemplates { get; set; } = [];
}