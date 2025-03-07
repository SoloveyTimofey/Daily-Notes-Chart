using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Exceptions;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Services;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Charts.Commands;

internal class CreateTimeOnlyChartCommandHandler : HandlerBase<ChartId>, ICommandHandler<CreateTimeOnlyChartCommand, ChartId>
{
    private readonly IChartRepository _chartRepository;
    private readonly IChartGroupRepository _chartGroupRepository;
    private readonly CreateChartService _createChartService;

    public CreateTimeOnlyChartCommandHandler(IChartRepository chartRepository, IChartGroupRepository chartGroupRepository, CreateChartService createChartService)
    {
        _chartRepository = chartRepository;
        _chartGroupRepository = chartGroupRepository;
        _createChartService = createChartService;
    }

    public async Task<Result<ChartId>> Handle(CreateTimeOnlyChartCommand request, CancellationToken cancellationToken)
    {
        var chartGroupId = new ChartGroupId(request.ChartGroupdId);

        if (await _chartGroupRepository.DoesEntityWithSpecifiedIdExistAsync(chartGroupId) is false)
        {
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(ChartGroup), request.ChartGroupdId.ToString());
        }

        var summaryResult = ChartSummary.Create(request.Summary);
        if(summaryResult.IsFailure) 
            return Failure(summaryResult);

        var dateResult = ChartDate.Create(request.Date);
        if(dateResult.IsFailure)
            return Failure(dateResult);

        var chartResult = await _createChartService.CreateTimeOnlyChartAsync(summaryResult.Value!, dateResult.Value!, chartGroupId);
        if (chartResult.IsFailure)
            return Failure(chartResult);

        await _chartRepository.CreateAsync(chartResult.Value!);

        return Result.Success(chartResult.Value!.Id);
    }
}