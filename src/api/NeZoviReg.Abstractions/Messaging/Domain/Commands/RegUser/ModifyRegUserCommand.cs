using NeZoviReg.Abstractions.Messaging.Domain.Model;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record ModifyRegUserCommand
    (Guid RegUserId,
        string? Name = default,
        string? Address = default,
        string? RegNumber = default,
        string? TaxNumber = default,
        string? UserName = default,
        string? Password = default,
        List<int>? Roles = default)
    : BaseCommand<RegUserDto>;
