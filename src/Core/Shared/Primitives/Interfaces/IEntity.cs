namespace Shared.Primitives.Interfaces;

public interface IEntity<out TId> where TId : ValueObject
{
    TId Id { get; }
    DateTimeOffset DateCreate { get; set; }
    DateTimeOffset? DateUpdate { get; set; }
}

public interface IEntity<out TId1, out TId2>
    where TId1 : ValueObject
    where TId2 : ValueObject
{
    TId1 Id1 { get; }
    TId2 Id2 { get; }
    DateTimeOffset DateCreate { get; set; }
    DateTimeOffset? DateUpdate { get; set; }
}
