using Shared.ValueObjects;

namespace Domain.UserManagement;
public class IdUserGroupMembership : BaseId<Guid>
{
    public IdUserGroupMembership()
        : base(Guid.NewGuid())
    {
        Value = Guid.NewGuid();
    }
    public IdUserGroupMembership(Guid value)
        : base(value)
    {
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
