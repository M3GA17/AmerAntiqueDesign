using Application.Abstractions;
using Application.Abstractions.Context;
using Domain.UserManagement;
using Domain.UserManagement.Repositories;

namespace Application.UserManagement.Queries.GetUsers;
internal class GetUsersListQueryHandler(IUserRepository userRepository, IContextService contextService) : IQueryHandler<GetUsersList.Query, List<User>>
{
    public async Task<List<User>> Handle(GetUsersList.Query request, CancellationToken cancellationToken)
    {
        var prova = contextService.IdUser;
        return (await userRepository.GetListAsync(cancellationToken)).ToList();
    }
}
