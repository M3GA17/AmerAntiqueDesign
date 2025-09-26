using Application.Abstractions;
using Domain.UserManagement;

namespace Application.UserManagement.Queries.GetUsers;
public class GetUsersList
{
    public class Query : IQuery<List<User>>
    {
    }
}
