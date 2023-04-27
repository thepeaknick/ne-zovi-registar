using NeZoviReg.Abstractions.Messaging.Domain.Model;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.User;


public record GetUserQuery(string PhoneNumber) : IQuery<UserDto>;
