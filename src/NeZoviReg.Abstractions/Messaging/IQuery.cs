using MediatR;
using NeZoviReg.Domain.Shared;

namespace NeZoviReg.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}