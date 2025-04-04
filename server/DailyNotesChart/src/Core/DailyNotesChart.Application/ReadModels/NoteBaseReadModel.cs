namespace DailyNotesChart.Application.ReadModels;

public abstract class NoteBaseReadModel
{
    public Guid ChartId { get; init; }
    public TimeOnly Time { get; init; }
    public string Color { get; init; } = null!;
    public string Description { get; init; } = null!;
}