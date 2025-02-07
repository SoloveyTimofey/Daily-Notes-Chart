namespace DailyNotesChart.WebApi.Requests.Charts;

public sealed record CreateTwoDimensionalChartRequest(
    DateOnly Date,
    Guid ChartGroupdId,
    string? Summary,
    string YAxeName,
    double Start,
    double End,
    bool IsInteger
);