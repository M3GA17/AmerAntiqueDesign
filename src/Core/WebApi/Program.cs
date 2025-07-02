using Application;
using Domain.UserManagement;
using Infrastructure;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //legacy settings for PostgreSql timestamp
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Add services to the container.
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddWebApi(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme)
                         .AddBearerToken(IdentityConstants.BearerScheme);
        builder.Services.AddIdentityCore<DomainUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
            });
        }

        app.UseHttpsRedirection();
        app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:3000"));

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGroup("api/identity")
           .WithTags("Identity")
           .MapIdentityApi<DomainUser>();

        app.MapControllers();

        app.Run();
    }
}
