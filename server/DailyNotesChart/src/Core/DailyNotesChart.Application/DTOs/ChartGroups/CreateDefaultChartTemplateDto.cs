namespace DailyNotesChart.Application.DTOs.ChartGroups;

public sealed record CreateDefaultChartTemplateDto(
    string YAxeName,
    double Start,
    double End,
    bool IsInteger
);