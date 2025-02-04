namespace DailyNotesChart.Application.DTOs;

public sealed record CreateDefaultChartTemplateDto(
    string YAxeName, 
    double Start, 
    double End, 
    bool IsInteger
);