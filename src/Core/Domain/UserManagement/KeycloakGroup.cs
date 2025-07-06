using Shared.Primitives;

namespace Domain.UserManagement;
public class KeycloakGroup : Entity<IdKeycloakGroup>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual ICollection<GroupRoleMapping> GroupRoleMappings { get; set; } = [];
    public virtual ICollection<UserGroupMembership> UserGroupMemberships { get; set; } = [];
}