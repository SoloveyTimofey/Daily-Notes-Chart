using DailyNotesChart.Domain.Abstractions;

namespace DailyNotesChart.Application.Abstractions.Persistance;

public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}