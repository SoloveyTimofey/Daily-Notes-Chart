using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.Exceptions;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.NoteTemplates.Commands;

internal sealed class CreateNoteTemplateCommandHandler : HandlerBase<NoteTemplateId>, ICommandHandler<CreateNoteTemplateCommand, NoteTemplateId>
{
    private readonly IChartGroupRepository _chartGroupRepository;

    public CreateNoteTemplateCommandHandler(IChartGroupRepository chartGroupRepository)
    {
        _chartGroupRepository = chartGroupRepository;
    }

    public async Task<Result<NoteTemplateId>> Handle(CreateNoteTemplateCommand request, CancellationToken cancellationToken)
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

        _chartGroupRepository.CreateNoteTemplate(noteTemplateResult.Value!);

        return Result.Success(noteTemplateResult.Value!.Id);
    }
}