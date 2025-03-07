using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Application.Exceptions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Domain.Shared.ResultPattern;

namespace DailyNotesChart.Application.Operations.ChartGroups.Commands;

internal sealed class SetDefaultNoteTemplateCommandHandler : HandlerBase<Result>, ICommandHandler<SetDefaultNoteTemplateCommand>
{
    private readonly IChartGroupRepository _chartGroupRepository;

    public SetDefaultNoteTemplateCommandHandler(IChartGroupRepository chartGroupRepository)
    {
        _chartGroupRepository = chartGroupRepository;
    }

    public async Task<Result> Handle(SetDefaultNoteTemplateCommand request, CancellationToken cancellationToken)
    {
        ChartGroup? chartGroup = await _chartGroupRepository.GetByIdAsync(request.ChartGroupId);

        if (chartGroup is null)
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(ChartGroup), request.ChartGroupId.Id.ToString());

        NoteTemplate? noteTemplate = await _chartGroupRepository.GetNoteTemplateByIdAsync(request.NoteTemplateId);
        if(noteTemplate is null)
            throw new EntityWithSpecifiedIdDoesNotExistException(nameof(NoteTemplate), request.NoteTemplateId.Id.ToString());

        var result = chartGroup.SetDefaultNoteTemplate(noteTemplate);

        if(result.IsFailure) return Failure(result);

        return Result.Success();
    }

}