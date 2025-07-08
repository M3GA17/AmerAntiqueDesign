using Shared.Primitives.Interfaces;

namespace Shared.Primitives;
public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>, IEntity<TId> where TId : ValueObject
{
    public virtual TId Id { get; } = id ?? throw new ArgumentNullException(nameof(id));
    public virtual DateTimeOffset DateCreate { get; set; }
    public virtual DateTimeOffset? DateUpdate { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other) return false;
        if (ReferenceEquals(this, other)) return true;
        return GetType() == other.GetType() && Id.Equals(other.Id);
    }
    public bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return GetType() == other.GetType() && Id.Equals(other.Id);
    }
    public override int GetHashCode()
    {
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


public abstract class Entity<TId1, TId2>(TId1 id1, TId2 id2) : IEquatable<Entity<TId1, TId2>>, IEntity<TId1, TId2>
    where TId1 : ValueObject
    where TId2 : ValueObject
{
    public virtual TId1 Id1 { get; } = id1 ?? throw new ArgumentNullException(nameof(id1));
    public virtual TId2 Id2 { get; } = id2 ?? throw new ArgumentNullException(nameof(id2));
    public virtual DateTimeOffset DateCreate { get; set; }
    public virtual DateTimeOffset? DateUpdate { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId1, TId2> other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return Id1.Equals(other.Id1) && Id2.Equals(other.Id2);
    }
    public bool Equals(Entity<TId1, TId2>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return Id1.Equals(other.Id1) && Id2.Equals(other.Id2);
    }
    public override int GetHashCode()
    {
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