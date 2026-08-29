using API.ValueTypes;
using MediatR;

namespace API.CQRS.Interfaces;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
