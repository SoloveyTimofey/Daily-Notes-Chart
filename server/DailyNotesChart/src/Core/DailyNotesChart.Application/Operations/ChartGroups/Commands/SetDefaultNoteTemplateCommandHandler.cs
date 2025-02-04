using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Shared;
using DailyNotesChart.Application.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;

namespace DailyNotesChart.Application.Operations.ChartGroups.Commands;

public sealed class SetDefaultNoteTemplateCommandHandler : CommandHandlerBase<Result>, ICommandHandler<SetDefaultNoteTemplateCommand>
{
    private readonly IChartGroupRepository _chartGroupRepository;
    private readonly INoteTemplateRepository _noteTemplateRepository;

    public SetDefaultNoteTemplateCommandHandler(IChartGroupRepository chartGroupRepository, INoteTemplateRepository noteTemplateRepository)
    {
        _chartGroupRepository = chartGroupRepository;
        _noteTemplateRepository = noteTemplateRepository;
    }

    public async Task<Result> Handle(SetDefaultNoteTemplateCommand request, CancellationToken cancellationToken)
    {
        ChartGroup? chartGroup = await _chartGroupRepository.GetByIdAsync(request.ChartGroupId);

        if (chartGroup is null)
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(ChartGroup), request.ChartGroupId.Id.ToString());

        NoteTemplate? noteTemplate = await _noteTemplateRepository.GetByIdAsync(request.NoteTemplateId);

        if(noteTemplate is null)
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(NoteTemplate), request.NoteTemplateId.Id.ToString());

        var result = chartGroup.SetDefaultNoteTemplate(noteTemplate);

        if(result.IsFailure) return Failure(result);

        return Result.Success();
    }

}