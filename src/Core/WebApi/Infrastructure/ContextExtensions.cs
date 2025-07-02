namespace WebApi.Infrastructure;

public static class ContextExtensions
{
    //public static IdUser GetIdUser(this IHttpContextAccessor httpContextAccessor) =>
    //    httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value is not null
    //        ? new IdUser(Guid.Parse(httpContextAccessor.HttpContext.User.FindFirst("sub").Value))
    //        : new IdUser(Guid.Empty);
}
