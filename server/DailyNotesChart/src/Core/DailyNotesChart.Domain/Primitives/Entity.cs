namespace DailyNotesChart.Domain.Primitives;

public abstract class Entity<TId> where TId : class
{
    #pragma warning disable
    protected Entity() { }
    #pragma warning enable
    protected Entity(TId id) => Id 
        = id;
    public TId Id { get; private init; }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }
    public static bool operator !=(Entity<TId>? first, Entity<TId>? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType()) return false;

        if (obj is not Entity<TId> entity) return false;

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id!.GetHashCode();
    }
}
