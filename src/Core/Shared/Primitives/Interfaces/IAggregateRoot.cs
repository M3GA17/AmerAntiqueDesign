namespace Shared.Primitives.Interfaces;

public interface IAggregateRoot<out TId, TIdUser> : IEntity<TId, TIdUser>
    where TId : ValueObject
    where TIdUser : ValueObject
{
    int DatabaseVersion { get; protected set; }
    void IncrementVersion();
}
