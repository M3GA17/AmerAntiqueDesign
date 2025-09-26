using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text.Json;

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
            options.Authority = builder.Configuration["Keycloak:authority"];
            options.Audience = builder.Configuration["Keycloak:resource"];
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
            // --- CODICE CORRETTO PER LEGGERE I REALM ROLES ---
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    if (context.Principal?.Identity is not ClaimsIdentity claimsIdentity)
                    {
                        return Task.CompletedTask;
                    }

                    // 1. Cerchiamo il claim "realm_access"
                    var realmAccessClaim = context.Principal.FindFirst("realm_access");
                    if (realmAccessClaim is null || string.IsNullOrWhiteSpace(realmAccessClaim.Value))
                    {
                        return Task.CompletedTask;
                    }

                    try
                    {
                        // 2. Eseguiamo il parsing del suo valore, che è una stringa JSON
                        using var realmAccessJson = JsonDocument.Parse(realmAccessClaim.Value);

                        // 3. Navighiamo il JSON per ottenere l'array "roles"
                        var roles = realmAccessJson.RootElement.GetProperty("roles").EnumerateArray();

                        // 4. Iteriamo sull'array e aggiungiamo ogni ruolo come ClaimTypes.Role
                        foreach (var role in roles)
                        {
                            var roleName = role.GetString();
                            if (!string.IsNullOrWhiteSpace(roleName))
                            {
                                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // È FONDAMENTALE loggare l'eccezione in caso di problemi col parsing.
                        // Potresti usare ILogger qui.
                        Console.WriteLine($"Errore durante il parsing dei ruoli da Keycloak: {ex.Message}");
                    }

                    return Task.CompletedTask;
                }
            };
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
                //c.OAuthClientSecret(builder.Configuration["Keycloak:client-secret"]);
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
