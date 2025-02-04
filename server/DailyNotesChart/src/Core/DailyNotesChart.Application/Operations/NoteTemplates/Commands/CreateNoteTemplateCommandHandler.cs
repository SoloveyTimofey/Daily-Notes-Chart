using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Exceptions;
using DailyNotesChart.Application.Operations.ChartGroups.Commands;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Application.Operations.NoteTemplates.Commands;

public sealed class CreateNoteTemplateCommandHandler : CommandHandlerBase<NoteTemplate>, ICommandHandler<CreateNoteTemplateCommand, NoteTemplate>
{
    private readonly IChartGroupRepository _chartGroupRepository;
    private readonly INoteTemplateRepository _noteTemplateRepository;

    public CreateNoteTemplateCommandHandler(IChartGroupRepository chartGroupRepository, INoteTemplateRepository noteTemplateRepository)
    {
        _chartGroupRepository = chartGroupRepository;
        _noteTemplateRepository = noteTemplateRepository;
    }

    public async Task<Result<NoteTemplate>> Handle(CreateNoteTemplateCommand request, CancellationToken cancellationToken)
    {
        if (await _chartGroupRepository.DoesEntityWithSpecifiedIdExistAsync(request.ChartGroupId) is false)
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(ChartGroup), request.ChartGroupId.Id.ToString());

        var colorResult = Color.Create(request.Color);
        if (colorResult.IsFailure) 
            return Failure(colorResult);

        var descriptionResult = NoteDescription.Create(request.NoteDescription);
        if (descriptionResult.IsFailure) 
            return Failure(descriptionResult);

        var noteTemplateResult = NoteTemplate.Create(request.ChartGroupId, colorResult.Value!, descriptionResult.Value!);
        if (noteTemplateResult.IsFailure) 
            return Failure(noteTemplateResult);

        _noteTemplateRepository.Create(noteTemplateResult.Value!);

        return Result.Success(noteTemplateResult.Value!);
    }
}