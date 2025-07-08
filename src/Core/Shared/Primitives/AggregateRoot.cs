using Shared.Primitives.Interfaces;

namespace Shared.Primitives;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot
    where TId : ValueObject
{
    private readonly IList<IDomainEvent> domainEvents = [];
    public virtual int DatabaseVersion { get; set; }

    public void IncrementVersion() => DatabaseVersion++;

    // Domain events
    public void RaiseDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => domainEvents.Clear();
}