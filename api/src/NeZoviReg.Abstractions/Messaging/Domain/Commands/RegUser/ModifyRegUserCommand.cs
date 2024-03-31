using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record ModifyRegUserCommand
    (Guid RegUserId,
        string? CompanyName = default,
        string? Email = default,
        string? Address = default,
        string? RegNumber = default,
        string? TaxNumber = default,
        string? FirstName = default,
        string? LastName = default,
        int? Role = default)
    : BaseCommand<RegUserDto>;
