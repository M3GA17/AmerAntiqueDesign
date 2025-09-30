namespace Shared.Primitives.Interfaces;

public interface IEntity<out TId>
    where TId : ValueObject
{
    TId Id { get; }
}

public interface IEntity<out TId1, out TId2>
    where TId1 : ValueObject
    where TId2 : ValueObject
{
    TId1 Id1 { get; }
    TId2 Id2 { get; }
}

