namespace WebApi.Infrastructure
{
    //public class ContextService(IHttpContextAccessor httpContextAccessor) : IContextService
    //{
    //    private readonly Lazy<IdUser> idUser = new Lazy<IdUser>(() =>
    //        {
    //            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    //            return userId is not null ? new IdUser(Guid.Parse(userId)) : new IdUser(Guid.Empty);
    //        });
    //    public IdUser IdUser => idUser.Value;
    //}
}
