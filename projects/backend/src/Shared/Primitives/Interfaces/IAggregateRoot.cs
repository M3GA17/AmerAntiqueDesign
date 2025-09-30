namespace Shared.Primitives.Interfaces;

public interface IAggregateRoot<out TId> : IEntity<TId>
    where TId : ValueObject
{
    int DatabaseVersion { get; protected set; }
    void IncrementVersion();
}

public interface IAggregateRoot<out TId1, out TId2> : IEntity<TId1, TId2>
    where TId1 : ValueObject
    where TId2 : ValueObject
{
    int DatabaseVersion { get; protected set; }
    void IncrementVersion();
}