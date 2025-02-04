using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Persistance.Context;

namespace DailyNotesChart.Persistance.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DailyNotesChartDbContext _context;

    public UnitOfWork(DailyNotesChartDbContext context)
    {
        _context = context;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}