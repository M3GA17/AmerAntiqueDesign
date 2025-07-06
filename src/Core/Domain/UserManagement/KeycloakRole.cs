using Shared.Primitives;

namespace Domain.UserManagement;
public class KeycloakRole : Entity<IdKeycloakRole>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } = [];
    public virtual ICollection<GroupRoleMapping> GroupRoleMappings { get; set; } = [];
}
