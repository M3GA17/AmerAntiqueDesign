using Application.Abstractions.Context;
using Domain.UserManagement;

namespace WebApi.Infrastructure
{
    public class ContextService : IContextService
    {
        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            idUser = new Lazy<IdUser>(() =>
            {
                var userId = httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;
                return userId is not null ? new IdUser(Guid.Parse(userId)) : new IdUser(Guid.Empty);
            });
        }

        private readonly Lazy<IdUser> idUser;
        public IdUser IdUser => idUser.Value;
    }
}
