using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DailyNotesChart.Persistance.Repositories;

internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId> 
    where TEntityId : class
{
    protected readonly DailyNotesChartDbContext Context;

    protected Repository(DailyNotesChartDbContext context)
    {
        Context = context;
    }

    public virtual Task<TEntity?> GetByIdAsync(TEntityId id)
    {
        //return Context.Set<TEntity>().SingleOrDefaultAsync(entity => entity.Id == id);

        return GetByIdAsync<TEntity, TEntityId>(id);
    }

    public virtual Task<bool> DoesEntityWithSpecifiedIdExistAsync(TEntityId id)
    {
        return Context.Set<TEntity>().AnyAsync(entity => entity.Id == id);
    }

    public virtual void Create(TEntity entity)
    {
        //Context.Set<TEntity>().Add(entity);
        Create<TEntity, TEntityId>(entity);
    }

    public virtual void Update(TEntity entity)
    {
        //Context.Set<TEntity>().Update(entity);
        Update<TEntity, TEntityId>(entity);
    }

    public virtual void Delete(TEntityId entityid)
    {
        //Context.Set<TEntity>().Remove(entity);
        Delete<TEntity, TEntityId>(entityid);
    }

    protected void Create<TAnotherEntity, TAnotherEntityId>(TAnotherEntity entity)
        where TAnotherEntity : Entity<TAnotherEntityId>
        where TAnotherEntityId : class
    {
        Context.Set<TAnotherEntity>().Add(entity);
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

    //protected IQueryable<TAnotherEntity> GetAllEntities<TAnotherEntity, TAnotherEntityId>()
    //    where TAnotherEntity : Entity<TAnotherEntityId>
    //    where TAnotherEntityId : class
    //{
    //    return Context.Set<TAnotherEntity>().AsQueryable();
    //}
}