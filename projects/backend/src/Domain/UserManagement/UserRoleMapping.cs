using Shared.Primitives;

namespace Domain.UserManagement;

public class UserRoleMapping : Entity<IdUser, IdKeycloakRole>
{
    public virtual User User { get; set; } = null!;
    public virtual KeycloakRole Role { get; set; } = null!;
}