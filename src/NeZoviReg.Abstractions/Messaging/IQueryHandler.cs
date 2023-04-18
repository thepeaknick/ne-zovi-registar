using MediatR;
using NeZoviReg.Domain.Shared;

namespace NeZoviReg.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}