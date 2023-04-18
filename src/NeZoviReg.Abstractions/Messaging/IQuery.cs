using MediatR;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}