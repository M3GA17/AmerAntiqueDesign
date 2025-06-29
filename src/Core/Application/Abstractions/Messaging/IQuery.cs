using MediatR;
using Shared.Base;

namespace Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
