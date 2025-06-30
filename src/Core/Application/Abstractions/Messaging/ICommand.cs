using MediatR;

namespace Application.Abstractions.Messaging;

//public interface ICommand : IRequest<Result>
//{
//}

//public interface ICommand<TResponse> : IRequest<Result<TResponse>>
//{
//}

public interface ICommand : IRequest<Unit>
{
}
public interface ICommand<TResponse> : IRequest<TResponse>
{
}
