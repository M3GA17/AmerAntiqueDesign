namespace Shared.Primitives.Interfaces;

public interface IAggregateRoot
{
    int DatabaseVersion { get; protected set; }
    void IncrementVersion();
    void RaiseDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
