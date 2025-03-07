using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Exceptions;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Services;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.Charts.Commands;

internal sealed class CreateTwoDimensionalChartCommandHandler : HandlerBase<ChartId>, ICommandHandler<CreateTwoDimensionalChartCommand, ChartId>
{
    private readonly IChartRepository _chartRepository;
    private readonly IChartGroupRepository _chartGroupRepository;
    private readonly CreateChartService _createChartService;
    public CreateTwoDimensionalChartCommandHandler(IChartRepository chartRepository, IChartGroupRepository chartGroupRepository, CreateChartService createChartService)
    {
        _chartRepository = chartRepository;
        _chartGroupRepository = chartGroupRepository;
        _createChartService = createChartService;
    }

    public async Task<Result<ChartId>> Handle(CreateTwoDimensionalChartCommand request, CancellationToken cancellationToken)
    {
        var chartGroupId = new ChartGroupId(request.ChartGroupdId);

        if (await _chartGroupRepository.DoesEntityWithSpecifiedIdExistAsync(chartGroupId) is false)
        {
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(ChartGroup), request.ChartGroupdId.ToString());
        }

        var summaryResult = ChartSummary.Create(request.Summary);
        if (summaryResult.IsFailure)
            return Failure(summaryResult);

        var dateResult = ChartDate.Create(request.Date);
        if (dateResult.IsFailure)
            return Failure(dateResult);

        var yAxeNameResult = YAxeName.Create(request.YAxeName);
        if (yAxeNameResult.IsFailure) 
            return Failure(yAxeNameResult);

        var yAxeValuesResult = YAxeValues.Create(request.Start, request.End, request.IsInteger);
        if(yAxeValuesResult.IsFailure)
            return Failure(yAxeValuesResult);

        var chartResult = await _createChartService.CreateTwoDimensionalChartAsync(summaryResult.Value!, dateResult.Value!, chartGroupId, yAxeValuesResult.Value!, yAxeNameResult.Value!);
        if (chartResult.IsFailure)
            return Failure(chartResult);

        await _chartRepository.CreateAsync(chartResult.Value!);

        return Result.Success(chartResult.Value!.Id);
    }
}