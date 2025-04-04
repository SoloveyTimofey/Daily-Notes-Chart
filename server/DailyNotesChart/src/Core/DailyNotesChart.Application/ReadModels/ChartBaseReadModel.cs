namespace DailyNotesChart.Application.ReadModels;

public abstract class ChartBaseReadModel
{
    public Guid Id { get; init; }
    public string? Summary { get; init; } = null!;
    public DateOnly Date { get; init; }
    public Guid ChartGroupId { get; init; }

    public List<NoteBaseReadModel> Notes { get; init; } = [];
}