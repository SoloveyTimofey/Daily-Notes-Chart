using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.ReadModels;
using DailyNotesChart.Domain.Shared.ResultPattern;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesChart.Application.Operations.Charts.Queries;

internal sealed class GetAllChartsForSpecifiedChartGroupQueryHandler : IQueryHandler<GetAllChartsForSpecifiedChartGroupQuery, List<ChartBaseReadModel>>
{
    private readonly IReadOnlyRepository _repository;

    public GetAllChartsForSpecifiedChartGroupQueryHandler(IReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ChartBaseReadModel>>> Handle(GetAllChartsForSpecifiedChartGroupQuery request, CancellationToken cancellationToken)
    {
        var chartsQuery = _repository.Charts;

        var chartsForSpecifiedChartGroupId = await chartsQuery.Where(c => c.ChartGroupId ==  request.ChartGroupId).ToListAsync();

        return Result.Success(chartsForSpecifiedChartGroupId);
    }
}