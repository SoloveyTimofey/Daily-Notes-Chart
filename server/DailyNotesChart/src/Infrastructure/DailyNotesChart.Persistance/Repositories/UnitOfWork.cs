using DailyNotesChart.Application.Abstractions.Persistance;
using DailyNotesChart.Domain.Abstractions;
using DailyNotesChart.Persistance.Contexts;

namespace DailyNotesChart.Persistance.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DailyNotesChartWriteDbContext _context;

    public UnitOfWork(DailyNotesChartWriteDbContext context)
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