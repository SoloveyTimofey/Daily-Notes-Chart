using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Application.Operations.Charts.Commands;

public sealed record CreateTimeOnlyChartCommand(
    DateOnly Date,
    Guid ChartGroupdId,
    string? Summary
) : ICommand<ChartId>;