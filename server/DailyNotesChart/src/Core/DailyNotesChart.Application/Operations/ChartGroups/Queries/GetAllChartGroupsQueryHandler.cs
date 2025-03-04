using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.DTOs.ChartGroups;
using Microsoft.EntityFrameworkCore;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Application.Operations.ChartGroups.Queries;

internal sealed class GetAllChartGroupsQueryHandler : IQueryHandler<GetAllChartGroupsQuery, List<ChartGroupReadDto>>
{
    private readonly IReadOnlyRepository _repository;

    public GetAllChartGroupsQueryHandler(IReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ChartGroupReadDto>>> Handle(GetAllChartGroupsQuery request, CancellationToken cancellationToken)
    {
        var chartGroupDtos = await _repository.ChartGroups
            .Select(chartGroup => new ChartGroupReadDto(chartGroup.Id, chartGroup.Name))
            .ToListAsync();

        return Result.Success(
            chartGroupDtos
        );
    }
}