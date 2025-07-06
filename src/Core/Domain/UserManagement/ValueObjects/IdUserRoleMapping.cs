using Shared.ValueObjects;

namespace Domain.UserManagement;

public sealed class IdUserRoleMapping : BaseId<Guid, Guid>
{
    public Guid UserId => Value1;
    public Guid RoleId => Value2;
    public IdUserRoleMapping(Guid userId, Guid roleId)
        : base(userId, roleId)
    {
        //if (userId == Guid.Empty)
        //    throw new ArgumentException("UserId cannot be empty", nameof(userId));

        //if (roleId == Guid.Empty)
        //    throw new ArgumentException("RoleId cannot be empty", nameof(roleId));

        Value1 = userId;
        Value2 = roleId;
    }

    public IdUserRoleMapping()
    : base(Guid.NewGuid(), Guid.NewGuid())
    {
        //if (userId == Guid.Empty)
        //    throw new ArgumentException("UserId cannot be empty", nameof(userId));

        //if (roleId == Guid.Empty)
        //    throw new ArgumentException("RoleId cannot be empty", nameof(roleId));

        Value1 = Guid.NewGuid();
        Value2 = Guid.NewGuid();
    }

}