namespace DailyNotesChart.Application.ReadModels;

public sealed class TwoDimensionalChartReadModel : ChartBaseReadModel
{
    public string YAxeName { get; set; } = string.Empty;
    public YAxeValuesReadModel YAxeValues { get; init; } = null!;
}