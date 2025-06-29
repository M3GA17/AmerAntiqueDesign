using Microsoft.AspNetCore.Identity;

namespace Domain.UserManagement;

public class DomainUser : IdentityUser<Guid>
{
    //[NotMapped]
    //public IdUser IdUser
    //{
    //    get { return new IdUser(Id); }
    //}
}

//public class Role : IdentityRole<Guid> { }

//public class UserClaim : IdentityUserClaim<Guid> { }

