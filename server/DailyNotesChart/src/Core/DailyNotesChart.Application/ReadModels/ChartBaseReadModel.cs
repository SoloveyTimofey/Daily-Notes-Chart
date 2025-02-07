using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;

namespace DailyNotesChart.Application.ReadModels;

public abstract class ChartBaseReadModel
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public string? Summary { get; set; }
    public Guid ChartGroupId { get; set; }
    public ChartGroupReadModel ChartGroup { get; set; } = null!;

    public IList<NoteBaseReadModel> Notes { get; set; } = [];
}