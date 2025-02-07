using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Persistance.Contexts;

namespace DailyNotesChart.Persistance.Repositories;

internal class ChartRepository : Repository<ChartBase, ChartId>, IChartRepository
{
    public ChartRepository(DailyNotesChartWriteDbContext context) : base(context) { }
}