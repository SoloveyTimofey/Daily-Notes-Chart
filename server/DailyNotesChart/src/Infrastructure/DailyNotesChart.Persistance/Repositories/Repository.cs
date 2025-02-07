using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesChart.Persistance.Repositories;

internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId> 
    where TEntityId : class
{
    protected readonly DailyNotesChartWriteDbContext Context;

    protected Repository(DailyNotesChartWriteDbContext context)
    {
        Context = context;
    }

    public virtual Task<TEntity?> GetByIdAsync(TEntityId id)
    {
        return GetByIdAsync<TEntity, TEntityId>(id);
    }

    public virtual Task<bool> DoesEntityWithSpecifiedIdExistAsync(TEntityId id)
    {
        return Context.Set<TEntity>().AnyAsync(entity => entity.Id == id);
    }

    public virtual void Create(TEntity entity)
    {
        Create<TEntity, TEntityId>(entity);
    }

    public virtual Task CreateAsync(TEntity entity)
    {
        return CreateAsync<TEntity, TEntityId>(entity);
    }

    public virtual void Update(TEntity entity)
    {
        Update<TEntity, TEntityId>(entity);
    }

    public virtual void Delete(TEntityId entityid)
    {
        Delete<TEntity, TEntityId>(entityid);
    }

    protected void Create<TAnotherEntity, TAnotherEntityId>(TAnotherEntity entity)
        where TAnotherEntity : Entity<TAnotherEntityId>
        where TAnotherEntityId : class
    {
        Context.Set<TAnotherEntity>().Add(entity);
    }

    protected async Task CreateAsync<TAnotherEntity, TAnotherEntityId>(TAnotherEntity entity)
        where TAnotherEntity : Entity<TAnotherEntityId>
        where TAnotherEntityId : class
    {
        await Context.Set<TAnotherEntity>().AddAsync(entity);
    }

    protected void Update<TAnotherEntity, TAnotherEntityId>(TAnotherEntity entity)
        where TAnotherEntity : Entity<TAnotherEntityId>
        where TAnotherEntityId : class
    {
        Context.Set<TAnotherEntity>().Update(entity);
    }

    protected void Delete<TAnotherEntity, TAnotherEntityId>(TAnotherEntityId entityId)
        where TAnotherEntity : Entity<TAnotherEntityId>
        where TAnotherEntityId : class
    {
        var entityToRemove = Context.Set<TAnotherEntity>().First(entity => entity.Id == entityId);
        Context.Set<TAnotherEntity>().Remove(entityToRemove);
    }

    protected Task<TAnotherEntity?> GetByIdAsync<TAnotherEntity, TAnotherEntityId>(TAnotherEntityId id)
        where TAnotherEntity : Entity<TAnotherEntityId>
        where TAnotherEntityId : class
    {
        return Context.Set<TAnotherEntity>().SingleOrDefaultAsync(entity => entity.Id == id);
    }
}