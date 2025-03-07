using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.ChartGroups;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

namespace DailyNotesChart.Application.Operations.ChartGroups.Commands;

public sealed record CreateChartGroupCommand(
    string Name,
    Guid CreatorId,
    CreateDefaultChartTemplateDto? DefaultChartTemplate = null,
    CreateNoteTemplateDto? DefaultNoteTemplate = null
) : ICommand<ChartGroupId>;