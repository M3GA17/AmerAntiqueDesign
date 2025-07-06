using Shared.ValueObjects;

namespace Domain.UserManagement;

public class IdKeycloakGroup : BaseId<Guid>
{
    public IdKeycloakGroup()
        : base(Guid.NewGuid())
    {
        Value = Guid.NewGuid();
    }
    public IdKeycloakGroup(Guid value)
        : base(value)
    {
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}