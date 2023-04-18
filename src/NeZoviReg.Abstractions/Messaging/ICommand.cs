using MediatR;
using NeZoviReg.Domain.Shared;

namespace NeZoviReg.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
