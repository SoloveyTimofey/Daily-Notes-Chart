namespace DailyNotesChart.Application.ReadModels;

public sealed record DefaultChartTemplateReadModel(string YAxeName, double Start, double End, bool IsInteger);