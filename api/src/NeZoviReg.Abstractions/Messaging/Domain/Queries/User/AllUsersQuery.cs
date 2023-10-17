using NeZoviReg.Abstractions.Extensions.Paging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.User;

public record AllUsersQuery(DateTime? After, PageInfo PageInfo) : IQuery<PagedList<UserInfoDto>>;
