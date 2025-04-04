﻿namespace DailyNotesChart.Domain.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : class
{
    protected AggregateRoot() { }
    protected AggregateRoot(TId id) : base(id) { }
}