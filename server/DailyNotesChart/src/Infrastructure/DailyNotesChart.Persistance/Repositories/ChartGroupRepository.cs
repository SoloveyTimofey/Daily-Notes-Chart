using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesChart.Persistance.Repositories;

internal class ChartGroupRepository : Repository<ChartGroup, ChartGroupId>, IChartGroupRepository
{
    public ChartGroupRepository(DailyNotesChartDbContext context) : base(context) { }

    public async Task<bool> ChartGroupWithSpecifiedIdExistsAsync(ChartGroupId id)
    {
        return await Context.Set<ChartGroup>().AnyAsync(x => x.Id == id);
    }
}