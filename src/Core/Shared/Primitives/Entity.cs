namespace Shared.Primitives;
public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : ValueObject
{
    public virtual TId Id { get; protected set; } = default!;
    public virtual DateTimeOffset DateCreate { get; set; }
    public virtual DateTimeOffset? DateUpdate { get; set; }

    protected Entity() { }

    protected Entity(TId id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Entity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        // Entità senza ID non sono mai uguali
        if (Id is null || other.Id is null)
            return false;

        return Id.Equals(other.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        if (Id is null || other.Id is null) return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        // Combina il tipo con l'ID per evitare collisioni tra tipi diversi
        return HashCode.Combine(GetType(), Id);
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }
}
public abstract class Entity<TId1, TId2> : IEquatable<Entity<TId1, TId2>>
    where TId1 : notnull
    where TId2 : notnull
{
    public virtual TId1 Id1 { get; protected set; }
    public virtual TId2 Id2 { get; protected set; }

    public virtual DateTimeOffset DateCreate { get; set; }
    public virtual DateTimeOffset? DateUpdate { get; set; }

    protected Entity() { }

    protected Entity(TId1 id1, TId2 id2)
    {
        Id1 = id1 ?? throw new ArgumentNullException(nameof(id1));
        Id2 = id2 ?? throw new ArgumentNullException(nameof(id2));
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Entity<TId1, TId2> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        // Entità senza ID non sono mai uguali
        if (Id1 is null || Id2 is null || other.Id1 is null || other.Id2 is null)
            return false;

        // Confronta entrambe le parti della chiave composta
        return Id1.Equals(other.Id1) && Id2.Equals(other.Id2);
    }

    public bool Equals(Entity<TId1, TId2>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        if (Id1 is null || Id2 is null || other.Id1 is null || other.Id2 is null) return false;

        return Id1.Equals(other.Id1) && Id2.Equals(other.Id2);
    }

    public override int GetHashCode()
    {
        // Combina il tipo con entrambe le parti della chiave
        return HashCode.Combine(GetType(), Id1, Id2);
    }

    public static bool operator ==(Entity<TId1, TId2>? left, Entity<TId1, TId2>? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId1, TId2>? left, Entity<TId1, TId2>? right)
    {
        return !(left == right);
    }
}