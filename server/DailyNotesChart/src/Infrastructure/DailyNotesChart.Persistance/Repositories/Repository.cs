using DailyNotesChart.Domain.Primitives;
using DailyNotesChart.Persistance.Context;
using Microsoft.EntityFrameworkCore;
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
        return Context.Set<TEntity>().SingleOrDefaultAsync(entity => entity.Id == id);
    }

    public virtual Task<bool> DoesEntityWithSpecifiedIdExistAsync(TEntityId id)
    {
        return Context.Set<TEntity>().AnyAsync(entity => entity.Id == id);
    }

    public virtual void Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }
}