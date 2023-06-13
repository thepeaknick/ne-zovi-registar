using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared.Model.Domain;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record AddUsersCommand(List<BulkUser> Users) : BaseCommand<List<UserDto>>;
