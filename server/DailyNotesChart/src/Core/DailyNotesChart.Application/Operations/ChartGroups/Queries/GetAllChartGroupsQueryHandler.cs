using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.DTOs.ChartGroups;
using Microsoft.EntityFrameworkCore;
using DailyNotesChart.Domain.Shared.ResultPattern;
using DailyNotesChart.Application.ReadModels;

namespace DailyNotesChart.Application.Operations.ChartGroups.Queries;

internal sealed class GetAllChartGroupsQueryHandler : IQueryHandler<GetAllChartGroupsQuery, List<ChartGroupReadModel>>
{
    private readonly IReadOnlyRepository _repository;

    public GetAllChartGroupsQueryHandler(IReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ChartGroupReadModel>>> Handle(GetAllChartGroupsQuery request, CancellationToken cancellationToken)
    {
        var chartGroups = _repository.ChartGroups;

        return Result.Success(
            await chartGroups.ToListAsync()
        );
    }
}