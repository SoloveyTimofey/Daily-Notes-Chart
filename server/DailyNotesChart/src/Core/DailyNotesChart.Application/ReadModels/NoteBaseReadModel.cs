namespace DailyNotesChart.Application.ReadModels;

public abstract class NoteBaseReadModel
{
    public string? Summary { get; set; }
    public DateOnly Date { get; set; }
    public Guid ChartId { get; set; }
}