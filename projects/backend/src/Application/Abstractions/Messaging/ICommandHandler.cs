using MediatR;

namespace Application.Abstractions.Messaging;

//public interface ICommandHandler<TCommand>
//    : IRequestHandler<TCommand, Result>
//    where TCommand : ICommand
//{
//}

//public interface ICommandHandler<TCommand, TResponse>
//    : IRequestHandler<TCommand, Result<TResponse>>
//    where TCommand : ICommand<TResponse>
//{
//}

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand
{
}
public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}