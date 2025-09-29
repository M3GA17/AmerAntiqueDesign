namespace Shared.Primitives.Interfaces;

public interface IAggregateRoot<out TId, TIdUser> : IEntity<TId, TIdUser>
    where TId : ValueObject
    where TIdUser : ValueObject
{
    int DatabaseVersion { get; protected set; }
    void IncrementVersion();
}

public interface IAggregateRoot<out TId1, out TId2, TIdUser> : IEntity<TId1, TId2, TIdUser>
    where TId1 : ValueObject
    where TId2 : ValueObject
    where TIdUser : ValueObject
{
    int DatabaseVersion { get; protected set; }
    void IncrementVersion();
}