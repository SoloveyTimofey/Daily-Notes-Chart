using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Application.ReadModels;

public sealed class YAxeValuesReadModel
{
    public YAxeValuesReadModel() { }
    public double Start { get; set; }
    public double End { get; set; }
    public bool IsInteger { get; set; }
}