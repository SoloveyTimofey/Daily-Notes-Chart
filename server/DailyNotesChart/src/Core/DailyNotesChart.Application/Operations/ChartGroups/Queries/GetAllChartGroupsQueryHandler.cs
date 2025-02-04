using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.ChartGroups;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Application.Operations.ChartGroups.Queries;

public sealed class GetAllChartGroupsQueryHandler : IQueryHandler<GetAllChartGroupsQuery, List<ChartGroupReadDto>>
{
    private readonly IChartGroupRepository _chartGroupRepository;

    public GetAllChartGroupsQueryHandler(IChartGroupRepository chartGroupRepository)
    {
        _chartGroupRepository = chartGroupRepository;
    }

    public async Task<Result<List<ChartGroupReadDto>>> Handle(GetAllChartGroupsQuery request, CancellationToken cancellationToken)
    {
        var chartGroups = _chartGroupRepository.GetAll()
            .Select(chartGroup => new ChartGroupReadDto(chartGroup.Id.Id, chartGroup.Name.Value))
            .ToList();

        return Result.Success(
            chartGroups.ToList()
        );
    }
}