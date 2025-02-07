namespace DailyNotesChart.Application.ReadModels;

public class NoteTemplateReadModel
{
    public Guid Id { get; set; }
    public Guid ChartGroupId { get;set; }
    public string Color { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}