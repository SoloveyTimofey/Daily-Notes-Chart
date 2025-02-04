using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;

namespace DailyNotesChart.Domain.Abstractions;

public interface IChartGroupRepository : ICrudRepository<ChartGroup, ChartGroupId>
{
}