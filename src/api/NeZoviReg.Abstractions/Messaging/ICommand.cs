using MediatR;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
    ICommand AddAppUser(string appUser);

    string AppUser { get; }
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
    ICommand<TResponse> AddAppUser(string appUser);

    string AppUser { get; }
}
