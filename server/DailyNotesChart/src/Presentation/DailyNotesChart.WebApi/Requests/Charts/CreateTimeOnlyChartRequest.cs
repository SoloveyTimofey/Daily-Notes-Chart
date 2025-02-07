namespace DailyNotesChart.WebApi.Requests.Charts;

public sealed record CreateTimeOnlyChartRequest(
    DateOnly Date,
    Guid ChartGroupdId,
    string? Summary
);