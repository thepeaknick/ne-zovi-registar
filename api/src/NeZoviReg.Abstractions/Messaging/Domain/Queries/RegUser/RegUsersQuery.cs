using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;

public record RegUsersQuery(RoleType Role) : IQuery<List<RegUserDto>>;
