using Shared.Primitives;

namespace Domain.UserManagement;
public class GroupRoleMapping : Entity<IdKeycloakGroup, IdKeycloakRole>
{
    public virtual KeycloakGroup Group { get; set; } = null!;
    public virtual KeycloakRole Role { get; set; } = null!;
}
