namespace Shared.Primitives;
public abstract class ValueObject : IEquatable<ValueObject>
{
    // Cache del hash per performance
    private int? CachedHashCode { get; set; }
    public abstract IEnumerable<object> GetEqualityComponents();
    public override bool Equals(object? obj)
    {
        return obj is ValueObject other &&
               GetType() == other.GetType() &&
               ValuesAreEqual(other);
    }
    public bool Equals(ValueObject? other)
    {
        return other is not null &&
               GetType() == other.GetType() &&
               ValuesAreEqual(other);
    }
    private bool ValuesAreEqual(ValueObject other)
    {
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }
    public override int GetHashCode()
    {
        CachedHashCode ??= GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate(HashCode.Combine);
        return CachedHashCode.Value;
    }


    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}
