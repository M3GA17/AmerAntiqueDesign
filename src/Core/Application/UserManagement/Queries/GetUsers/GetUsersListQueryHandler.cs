using Application.Abstractions;
using Application.Abstractions.Context;
using Domain.UserManagement;
using Domain.UserManagement.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.UserManagement.Queries.GetUsers;
internal class GetUsersListQueryHandler(IUserRepository userRepository, IContextService contextService, IHttpContextAccessor httpContextAccessor) : IQueryHandler<GetUsersList.Query, List<User>>
{
    public async Task<List<User>> Handle(GetUsersList.Query request, CancellationToken cancellationToken)
    {
        return (await userRepository.GetListAsync(cancellationToken)).ToList();
    }
}
