using DailyNotesChart.Application.Abstractions.MediatrSpecific;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Application.Operations.Charts.Commands;

public sealed record CreateTwoDimensionalChartCommand(
    DateOnly Date,
    Guid ChartGroupdId,
    string? Summary,
    string YAxeName,
    double Start,
    double End,
    bool IsInteger
) : ICommand<ChartId>;