namespace DailyNotesChart.Application.ReadModels;

public sealed class YAxeValuesReadModel
{
    public YAxeValuesReadModel() { }
    public double Start { get; init; }
    public double End { get; init; }
    public bool IsInteger { get; init; }
}