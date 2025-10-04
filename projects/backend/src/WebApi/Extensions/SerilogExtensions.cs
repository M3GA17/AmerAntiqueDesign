using Serilog;
using Serilog.Events;

namespace WebApi.Extensions
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
            {
                var connectionString = context.Configuration.GetConnectionString("Default"); //from secret
                loggerConfiguration

                    // minimum level
                    .MinimumLevel.Information()

                    // override namespaces
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)

                    // Enrichers
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithProcessId()
                    .Enrich.WithThreadId()

                     //Sink PostgreSQL - just errors
                     .WriteTo.PostgreSQL(
                         connectionString: connectionString,
                         tableName: "errors_tb",
                         schemaName: "log",
                         needAutoCreateTable: true,
                         restrictedToMinimumLevel: LogEventLevel.Error
                     ).WriteTo.Console();

                //.WriteTo.Console();
                //// Secondo Sink PostgreSQL - Da Information in su
                //.WriteTo.PostgreSQL(
                //    connectionString: connectionString,
                //    tableName: "audit_trails_tb",
                //    schemaName: "log",
                //    needAutoCreateTable: true,
                //    restrictedToMinimumLevel: LogEventLevel.Information
                //);
            });
            return builder;
        }
    }
}
