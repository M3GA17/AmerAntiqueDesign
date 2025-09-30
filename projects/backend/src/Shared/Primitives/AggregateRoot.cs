using Shared.Primitives.Interfaces;

namespace Shared.Primitives;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : ValueObject
{
    public virtual int DatabaseVersion { get; set; }
    public void IncrementVersion() => DatabaseVersion++;
}

public abstract class AggregateRoot<TId1, TId2>(TId1 id1, TId2 id2) : Entity<TId1, TId2>(id1, id2), IAggregateRoot<TId1, TId2>
    where TId1 : ValueObject
    where TId2 : ValueObject
{
    public virtual int DatabaseVersion { get; set; }
    public void IncrementVersion() => DatabaseVersion++;
}