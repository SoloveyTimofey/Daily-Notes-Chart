using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Application.Operations.ChartGroups.Commands;

public record SetDefaultNoteTemplateCommand(
    NoteTemplateId NoteTemplateId,
    ChartGroupId ChartGroupId
) : ICommand;