namespace DailyNotesChart.Application.ReadModels;

public class TwoDimensionalChartReadModel : ChartBaseReadModel
{
    public YAxeValuesReadModel YAxeValues { get; set; } = null!;
    public string YAxeName { get; set; } = string.Empty;
}