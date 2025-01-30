using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;

namespace DailyNotesChart.Domain.Abstractions;

public interface IChartRepository : IRepository
{
    YAxeValues GetYAxeValueOfSpecifiedChart(ChartId chartId);
}