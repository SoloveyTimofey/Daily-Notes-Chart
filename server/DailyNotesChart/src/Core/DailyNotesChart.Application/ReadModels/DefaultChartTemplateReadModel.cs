namespace DailyNotesChart.Application.ReadModels;

public sealed class DefaultChartTemplateReadModel
{
    public string YAxeName { get; init; } = null!;
    public double Start {  get; init; }
    public double End { get; init; }
    public bool IsInteger { get; init; }
};