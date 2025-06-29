namespace Shared.Primitives;
public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : ValueObject
{
    public virtual TId Id { get; protected set; } = default!;
    public virtual DateTimeOffset DateCreate { get; set; }
    public virtual DateTimeOffset DateUpdate { get; set; }

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