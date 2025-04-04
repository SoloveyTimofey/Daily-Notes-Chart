namespace DailyNotesChart.Application.ReadModels;

public sealed class NoteTemplateReadModel
{
    public Guid Id { get; init; }
    public Guid ChartGroupId { get; init; }
    public string Color { get; init; } = null!;
    public string Description { get; init; } = null!;
}