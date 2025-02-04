using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Application.DTOs;
using DailyNotesChart.Application.Operations.NoteTemplates.Commands;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Domain.Shared;
using MediatR;

namespace DailyNotesChart.Application.Operations.ChartGroups.Commands;

// Base Class
internal sealed class CreateChartGroupCommandHandler : CommandHandlerBase<ChartGroup>, ICommandHandler<CreateChartGroupCommand, ChartGroup>
{
    private readonly IChartGroupRepository _chartGroupRepository;
    private readonly INoteTemplateRepository _noteTemplateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChartGroupCommandHandler(IChartGroupRepository chartGroupRepository, INoteTemplateRepository noteTemplateRepository, IUnitOfWork unitOfWork)
    {
        _chartGroupRepository = chartGroupRepository;
        _noteTemplateRepository = noteTemplateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ChartGroup>> Handle(CreateChartGroupCommand request, CancellationToken cancellationToken)
    {
        var nameResult = ChartGroupName.Create(request.Name);
        if (nameResult.IsFailure)
            return await FailureAsync(nameResult);

        Result<ChartGroup> chartGroupResult;

        if (request.DefaultChartTemplate is not null)
        {
            var defaultChartTemplateResult = CreateDefaultChartTemplate(request.DefaultChartTemplate);
            if (defaultChartTemplateResult.IsFailure)
                return await FailureAsync(defaultChartTemplateResult);

            chartGroupResult = ChartGroup.Create(nameResult.Value!, defaultChartTemplateResult.Value);
        }
        else
        {
            chartGroupResult = ChartGroup.Create(nameResult.Value!);
        }

        if (chartGroupResult.IsFailure) return await FailureAsync(chartGroupResult);

        ChartGroup chartGroup = chartGroupResult.Value!;

        _chartGroupRepository.Create(chartGroup);
        await _unitOfWork.SaveChangesAsync();

        if (request.DefaultNoteTemplate is not null)
        {
            var noteTemplateResult = CreateDefaultNoteTemplate(request.DefaultNoteTemplate, chartGroup.Id);
            if (noteTemplateResult.IsFailure)
                return await FailureAsync(noteTemplateResult);

            var setDefaultNoteTemplateResult = chartGroup.SetDefaultNoteTemplate(noteTemplateResult.Value!);
            if (setDefaultNoteTemplateResult.IsFailure)
                return await FailureAsync(setDefaultNoteTemplateResult);
        }

        return await Task.FromResult(Result.Success(chartGroup));
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

    private Result<NoteTemplate> CreateDefaultNoteTemplate(CreateNoteTemplateDto createParams, ChartGroupId chartGroupId)
    {
        var colorResult = Color.Create(createParams.Color);
        if (colorResult.IsFailure)
            return Failure<NoteTemplate>(colorResult);

        var noteDescriptionResult = NoteDescription.Create(createParams.NoteDescription);
        if (noteDescriptionResult.IsFailure)
            return Failure<NoteTemplate>(noteDescriptionResult);

        var noteTemplateResult = NoteTemplate.Create(chartGroupId, colorResult.Value!, noteDescriptionResult.Value!);
        if (noteTemplateResult.IsFailure)
            return Failure<NoteTemplate>(noteTemplateResult);

        _noteTemplateRepository.Create(noteTemplateResult.Value!);

        return Result.Success(noteTemplateResult.Value!);
    }
}