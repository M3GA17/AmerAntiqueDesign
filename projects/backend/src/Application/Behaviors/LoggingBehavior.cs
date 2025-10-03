using MediatR;

namespace Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Log prima dell'esecuzione
        var response = await next(cancellationToken);
        // Log dopo l'esecuzione
        return response;
    }
}