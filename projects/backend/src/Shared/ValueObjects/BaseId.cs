using Shared.Primitives;
using System.Diagnostics;

namespace Shared.ValueObjects;

[DebuggerDisplay("{Value}")]
public abstract class BaseId<T>(T id) : ValueObject
    where T : notnull
{
    public T Value { get; protected set; } = id ?? throw new ArgumentNullException(nameof(id));

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value?.ToString() ?? string.Empty;
}

[DebuggerDisplay("{Value1}-{Value2}")]
public abstract class BaseId<T1, T2>(T1 value1, T2 value2) : ValueObject
    where T1 : notnull
    where T2 : notnull
{
    public T1 Value1 { get; protected set; } = value1 ?? throw new ArgumentNullException(nameof(value1));
    public T2 Value2 { get; protected set; } = value2 ?? throw new ArgumentNullException(nameof(value2));

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value1!;
        yield return Value2!;
    }
    public override string ToString()
    {
        return $"{Value1}-{Value2}";
    }
}