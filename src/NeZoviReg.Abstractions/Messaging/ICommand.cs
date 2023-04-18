using MediatR;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
