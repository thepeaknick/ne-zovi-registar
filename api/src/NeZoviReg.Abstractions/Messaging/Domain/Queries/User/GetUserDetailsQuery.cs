using NeZoviReg.Abstractions.Messaging.Domain.Model.User;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.User;

public record GetUserDetailsQuery(string PhoneNumber) : IQuery<UserDetailsDto>;
