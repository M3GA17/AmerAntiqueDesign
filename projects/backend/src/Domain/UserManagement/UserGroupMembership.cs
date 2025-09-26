using Shared.Primitives;

namespace Domain.UserManagement;

public class UserGroupMembership : Entity<IdUser, IdKeycloakGroup>
{
    public virtual User User { get; set; } = null!;
    public virtual KeycloakGroup Group { get; set; } = null!;
}