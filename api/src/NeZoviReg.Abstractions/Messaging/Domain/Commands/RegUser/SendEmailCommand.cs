namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record SendEmailCommand(string? FirstName, string? LastName, string? CompanyName, string EmailFrom, string PhoneNumber, string Content)
    : BaseCommand<bool>;
