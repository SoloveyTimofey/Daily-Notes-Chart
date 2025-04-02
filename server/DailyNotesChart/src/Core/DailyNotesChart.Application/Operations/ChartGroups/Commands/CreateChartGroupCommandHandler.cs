using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.DTOs.ChartGroups;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.ChartGroups.Commands;

// Base Class
internal sealed class CreateChartGroupCommandHandler : HandlerBase<ChartGroupId>, ICommandHandler<CreateChartGroupCommand, ChartGroupId>
{
    private readonly IChartGroupRepository _chartGroupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChartGroupCommandHandler(IChartGroupRepository chartGroupRepository, IUnitOfWork unitOfWork)
    {
        _chartGroupRepository = chartGroupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ChartGroupId>> Handle(CreateChartGroupCommand request, CancellationToken cancellationToken)
    {
        var nameResult = ChartGroupName.Create(request.Name);
        if (nameResult.IsFailure)
            return await FailureAsync(nameResult);

        Result<ChartGroup> chartGroupResult;
        var creatorId = new ApplicationUserId(request.CreatorId);
        if (request.DefaultChartTemplate is not null)
        {
            var defaultChartTemplateResult = CreateDefaultChartTemplate(request.DefaultChartTemplate);
            if (defaultChartTemplateResult.IsFailure)
                return await FailureAsync(defaultChartTemplateResult);

            chartGroupResult = ChartGroup.Create(nameResult.Value!, creatorId, defaultChartTemplateResult.Value);
        }
        else
        {
            chartGroupResult = ChartGroup.Create(nameResult.Value!, creatorId);
        }

        if (chartGroupResult.IsFailure) return await FailureAsync(chartGroupResult);

        ChartGroup chartGroup = chartGroupResult.Value!;

        _chartGroupRepository.Create(chartGroup);

        return await Task.FromResult(Result.Success(chartGroup.Id));
    }

    private Result<DefaultChartTemplate> CreateDefaultChartTemplate(CreateDefaultChartTemplateDto createParams)
    {
        var yAxeNameResult = YAxeName.Create(createParams.YAxeName);
        if (yAxeNameResult.IsFailure)
            return Failure<DefaultChartTemplate>(yAxeNameResult);

        var yAxeValuesResult = YAxeValues.Create(createParams.Start, createParams.End, createParams.IsInteger);
        if (yAxeValuesResult.IsFailure)
            return Failure<DefaultChartTemplate>(yAxeValuesResult);

        var defaultChartTemplate = new DefaultChartTemplate(yAxeNameResult.Value!, yAxeValuesResult.Value!);

        return Result.Success(defaultChartTemplate);
    }
}