using Application.Abstractions.UnitOfWork;
using Domain.ProductManagement.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddMediatR(config =>
        //{
        //    config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        //});

        #region DbContext
        string? connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        #endregion DbContext

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
