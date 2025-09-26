using MediatR;

namespace Application.Abstractions;

//public interface IQuery<TResponse> : IRequest<Result<TResponse>>
//{
//}

public interface IQuery<TResponse> : IRequest<TResponse>
{
}

