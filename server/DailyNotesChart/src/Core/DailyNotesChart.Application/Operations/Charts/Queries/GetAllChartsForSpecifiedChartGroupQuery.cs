using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.Charts;

namespace DailyNotesChart.Application.Operations.Charts.Queries;

public sealed record GetAllChartsForSpecifiedChartGroupQuery(
    Guid ChartGroupId
) : IQuery<List<ChartReadDto>>;