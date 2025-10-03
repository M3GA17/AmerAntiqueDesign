using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApi.Extensions;
using WebApi.Middleware;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        //Serilog.Debugging.SelfLog.Enable(Console.Error);

        try
        {
            // Log di avvio applicazione
            Log.Information("Starting...");

            var builder = WebApplication.CreateBuilder(args);

            //legacy settings for PostgreSql timestamp
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Configure Serilog
            builder.ConfigureSerilog();

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

            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseSerilogRequestLogging();
            //app.UseSerilogRequestLogging(options =>
            //{
            //    options.MessageTemplate =
            //        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
            //    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            //    {
            //        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            //        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
            //    };
            //});

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
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

            app.MapControllers();
            Log.Error("wewewe");
            app.Run();

            Log.Information("Ending...");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
