using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;

public record DefaultChartTemplate(YAxeName YAxeName, YAxeValues YAxeValues);