using API.ValueTypes;
using MediatR;

namespace API.CQRS.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
