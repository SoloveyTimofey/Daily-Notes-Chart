namespace DailyNotesChart.Domain.Abstractions;

public interface ICrudRepository<TEntity, TEntityId>
{
    Task<TEntity?> GetByIdAsync(TEntityId id);
    Task<bool> DoesEntityWithSpecifiedIdExistAsync(TEntityId id);
    void Update(TEntity chartGroup);
    void Create(TEntity chartGroup);
    void Delete(TEntity chartGroup);
}