using Application.Abstractions;
using Domain.UserManagement;
using Domain.UserManagement.Repositories;

namespace Application.UserManagement.Queries.GetUsers;
internal class GetUsersListQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUsersList.Query, List<User>>
{
    public async Task<List<User>> Handle(GetUsersList.Query request, CancellationToken cancellationToken)
    {
        return (await userRepository.GetListAsync(cancellationToken)).ToList();
    }
}
