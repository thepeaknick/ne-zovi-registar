namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.User;

public record AllUsersQuery(DateTime? After) : IQuery<List<string>>;
