using Shared.Primitives;

namespace Domain.UserManagement;

public class User : AggregateRoot<IdUser>
{
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsEnabled { get; set; }
    public override int DatabaseVersion
    {
        get => 0;
    }

    public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } = [];
    public virtual ICollection<UserGroupMembership> UserGroupMemberships { get; set; } = [];
}
