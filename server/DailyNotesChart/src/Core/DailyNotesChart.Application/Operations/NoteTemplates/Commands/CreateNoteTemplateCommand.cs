using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteTemplateCluster;

namespace DailyNotesChart.Application.Operations.NoteTemplates.Commands;

public sealed record CreateNoteTemplateCommand(
    ChartGroupId ChartGroupId,
    string Color,
    string NoteDescription
) : ICommand<NoteTemplateId>;