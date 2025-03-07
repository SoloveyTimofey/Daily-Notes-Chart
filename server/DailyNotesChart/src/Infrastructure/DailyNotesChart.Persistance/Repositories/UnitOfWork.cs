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

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}