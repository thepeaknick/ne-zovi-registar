namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record ModifyUserCommand(string PhoneNumber, string? FirstName = null, string? LastName = null, string? Jmbg = null, string? NewPhoneNumber = null)
    : BaseCommand<string>;
