using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Domain.Abstractions;

public interface IChartRepository : ICrudRepository<ChartBase, ChartId>
{
}