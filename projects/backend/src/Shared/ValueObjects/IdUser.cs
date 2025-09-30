namespace Shared.ValueObjects;

public class IdUser : BaseId<Guid>
{
    public IdUser()
        : base(Guid.NewGuid())
    {
        Value = Guid.NewGuid();
    }
    public IdUser(Guid value)
        : base(value)
    {
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
