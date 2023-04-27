namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record ModifyRegUserCommand
    (Guid RegUserId, string? Email = default, string? UserName = default, string? Password = default, string? FirstName = default, string? LastName = default, List<int>? Roles = default)
    : BaseCommand<string>;
