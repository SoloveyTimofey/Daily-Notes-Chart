using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Application.DTOs.ChartGroups;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;

namespace DailyNotesChart.Application.Operations.ChartGroups.Commands;

public sealed record CreateChartGroupCommand(
    string Name,
    CreateDefaultChartTemplateDto? DefaultChartTemplate = null,
    CreateNoteTemplateDto? DefaultNoteTemplate = null
) : ICommand<ChartGroup>;