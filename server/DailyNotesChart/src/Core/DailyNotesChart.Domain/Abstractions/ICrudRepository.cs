namespace DailyNotesChart.Domain.Abstractions;

public interface ICrudRepository<TEntity, TEntityId>
{
    Task<TEntity?> GetByIdAsync(TEntityId id);
    Task<bool> DoesEntityWithSpecifiedIdExistAsync(TEntityId id);
    void Update(TEntity entity);
    void Create(TEntity entity);
    Task CreateAsync(TEntity entity);
    void Delete(TEntityId entityId);
}