namespace Shared.Primitives.Interfaces;

public interface IEntity<out TId, TIdUser>
    where TId : ValueObject
    where TIdUser : ValueObject
{
    TId Id { get; }

    DateTimeOffset DateCreate { get; set; }
    TIdUser IdUserCreate { get; set; }
    DateTimeOffset? DateUpdate { get; set; }
    TIdUser? IdUserUpdate { get; set; }
}

public interface IEntity<out TId1, out TId2, TIdUser>
    where TId1 : ValueObject
    where TId2 : ValueObject
    where TIdUser : ValueObject
{
    TId1 Id1 { get; }
    TId2 Id2 { get; }

    DateTimeOffset DateCreate { get; set; }
    TIdUser IdUserCreate { get; set; }
    DateTimeOffset? DateUpdate { get; set; }
    TIdUser? IdUserUpdate { get; set; }
}

