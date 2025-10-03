namespace WebApi.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    private readonly RequestDelegate next = next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // L'utente viene recuperato dal contesto della richiesta (se autenticato)
            var userName = context.User?.Identity?.Name ?? "Anonymous";
            var clientIp = context.Connection.RemoteIpAddress?.ToString();

            // Serilog arricchisce il log con le informazioni che abbiamo configurato
            logger.LogError(ex,
                "Unhandled exception for request {RequestMethod} {RequestPath} by user {UserName} from {ClientIp}",
                context.Request.Method, context.Request.Path, userName, clientIp);

            // Qui Serilog, grazie alla configurazione, scriverà sulla tabella log.errors_tb
            // perché il livello è "Error".

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An unexpected error occurred.");








            //logger.LogError(ex, "Errore non gestito");
            //// Qui puoi salvare l'errore su DB
            //// await _errorLoggingService.LogErrorAsync(ex);
            //context.Response.StatusCode = 500;
            //await context.Response.WriteAsync("Internal Server Error");
        }
    }
}