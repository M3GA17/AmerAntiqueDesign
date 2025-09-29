using Shared.Base.Interfaces;

namespace Domain.UserManagement.Repositories;

public interface IUserRepository : IBaseRepository<User, IdUser>
{
}
