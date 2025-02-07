using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.DTOs.Charts;
using DailyNotesChart.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesChart.Application.Operations.Charts.Queries;

internal sealed class GetAllChartsForSpecifiedChartGroupQueryHandler : IQueryHandler<GetAllChartsForSpecifiedChartGroupQuery, List<ChartReadDto>>
{
    private readonly IReadOnlyRepository _repository;

    public GetAllChartsForSpecifiedChartGroupQueryHandler(IReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ChartReadDto>>> Handle(GetAllChartsForSpecifiedChartGroupQuery request, CancellationToken cancellationToken)
    {
        var timeOnlyChartQuery = _repository.TimeOnlyCharts
            .Where(chart => chart.ChartGroupId == request.ChartGroupId)
            .Select(chart => new TimeOnlyChartReadDto(chart.Id, chart.Date, chart.ChartGroupId, chart.Notes.ToList()));

        var twoDimensionalChartQuery = _repository.TwoDimensionalCharts
            .Where(chart => chart.ChartGroupId == request.ChartGroupId)
            .Select(chart => new TwoDimensionalChartReadDto(chart.Id, chart.Date, chart.ChartGroupId, chart.Notes.ToList(), chart.YAxeValues, chart.YAxeName));

        List<ChartReadDto> charts = await timeOnlyChartQuery.Cast<ChartReadDto>().Concat(twoDimensionalChartQuery.Cast<ChartReadDto>())
            .OrderBy(chart => chart.Date)
            .ToListAsync();

        return Result.Success(charts);
    }
}