namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddWebApi(
        this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped<IContextService, ContextService>();
    }
}
