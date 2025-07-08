using Shared.Primitives.Interfaces;

namespace Shared.Primitives;

public abstract class AggregateRoot<TId, TIdUser>(TId id) : Entity<TId, TIdUser>(id), IAggregateRoot<TId, TIdUser>
    where TId : ValueObject
    where TIdUser : ValueObject
{
    public virtual int DatabaseVersion { get; set; }
    public void IncrementVersion() => DatabaseVersion++;
}

