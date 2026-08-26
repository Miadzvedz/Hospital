using API.ValueTypes;
using MediatR;

namespace API.CQRS.Interfaces;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
