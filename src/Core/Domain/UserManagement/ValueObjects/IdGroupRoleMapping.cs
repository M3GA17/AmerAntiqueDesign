using Shared.ValueObjects;

namespace Domain.UserManagement;
public class IdGroupRoleMapping : BaseId<Guid>
{
    public IdGroupRoleMapping()
        : base(Guid.NewGuid())
    {
        Value = Guid.NewGuid();
    }
    public IdGroupRoleMapping(Guid value)
        : base(value)
    {
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
