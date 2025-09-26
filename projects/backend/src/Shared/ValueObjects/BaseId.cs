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
public abstract class BaseId<T1, T2> : ValueObject
{
    public virtual T1 Value1 { get; protected set; }
    public virtual T2 Value2 { get; protected set; }
    protected BaseId(T1 value1, T2 value2)
    {
        Value1 = value1 ?? throw new ArgumentNullException(nameof(value1));
        Value2 = value2 ?? throw new ArgumentNullException(nameof(value2));
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value1!;
        yield return Value2!;
    }
    //public override string ToString()
    //{
    //    return $"{Value1}-{Value2}";
    //}
}