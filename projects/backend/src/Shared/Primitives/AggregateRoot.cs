using Shared.Primitives.Interfaces;

namespace Shared.Primitives;

public abstract class AggregateRoot<TId, TIdUser>(TId id) : Entity<TId, TIdUser>(id), IAggregateRoot<TId, TIdUser>
    where TId : ValueObject
    where TIdUser : ValueObject
{
    public virtual int DatabaseVersion { get; set; }
    public void IncrementVersion() => DatabaseVersion++;
}

public abstract class AggregateRoot<TId1, TId2, TIdUser>(TId1 id1, TId2 id2) : Entity<TId1, TId2, TIdUser>(id1, id2), IAggregateRoot<TId1, TId2, TIdUser>
    where TId1 : ValueObject
    where TId2 : ValueObject
    where TIdUser : ValueObject
{
    public virtual int DatabaseVersion { get; set; }
    public void IncrementVersion() => DatabaseVersion++;
}