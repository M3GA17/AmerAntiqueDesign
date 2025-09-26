using Shared.ValueObjects;

namespace Domain.UserManagement;
public class IdKeycloakRole : BaseId<Guid>
{
    public IdKeycloakRole()
        : base(Guid.NewGuid())
    {
        Value = Guid.NewGuid();
    }
    public IdKeycloakRole(Guid value)
        : base(value)
    {
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
