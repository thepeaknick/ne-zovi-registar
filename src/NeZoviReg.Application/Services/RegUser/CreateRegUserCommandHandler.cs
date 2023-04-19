using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class CreateRegUserCommandHandler : ICommandHandler<CreateRegUserCommand, string>
{
    public async Task<Result<string>> Handle(CreateRegUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
