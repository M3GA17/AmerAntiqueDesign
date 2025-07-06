using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        builder.Services.AddControllers()
              .AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                  options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
              });
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            // L'URL del tuo realm Keycloak
            options.Authority = builder.Configuration["Keycloak:authority"];
            // Il Client ID della tua API
            options.Audience = builder.Configuration["Keycloak:resource"];
            options.RequireHttpsMetadata = false; // Metti a true in produzione
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });

            // Definisci lo schema di sicurezza OAuth2 per Keycloak
            option.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        // URL di login di Keycloak
                        AuthorizationUrl = new Uri(builder.Configuration["Keycloak:auth-server-url"]!),
                        // URL per lo scambio del token
                        TokenUrl = new Uri(builder.Configuration["Keycloak:token-url"]!),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "openid scope" },
                            { "profile", "profile scope" }
                        }
                    }
                }
            });

            // Applica la sicurezza a tutti gli endpoint
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Keycloak" }
                    },
                    new string[] {}
                }
            });
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");

                // Configurazione OAuth esistente
                c.OAuthClientId(builder.Configuration["Keycloak:resource"]);
                c.OAuthAppName("My API - Swagger UI");
                c.OAuthUsePkce();

                // Aggiungi questa riga per il Client Secret
                c.OAuthClientSecret(builder.Configuration["Keycloak:client-secret"]);
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

        //app.MapGroup("api/identity")
        //   .WithTags("Identity")
        //   .MapIdentityApi<DomainUser>();

        app.MapControllers();

        app.Run();
    }
}
