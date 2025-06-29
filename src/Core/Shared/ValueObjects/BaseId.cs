using Shared.Primitives;

namespace Shared.ValueObjects;
public abstract class BaseId<T>(T id) : ValueObject
{
    public virtual T Value { get; protected set; } = id ?? throw new ArgumentNullException(nameof(id));
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value!; //TO CHECK '!'
    }
}
