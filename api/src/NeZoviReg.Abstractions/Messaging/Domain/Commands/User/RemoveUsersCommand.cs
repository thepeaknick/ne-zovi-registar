using NeZoviReg.Abstractions.Messaging.Domain.Model.User;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record RemoveUsersCommand(List<string> PhoneNumbers) : BaseCommand<bool>;
