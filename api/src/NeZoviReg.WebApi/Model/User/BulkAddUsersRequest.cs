using NeZoviReg.Abstractions.Shared.Model.Domain;

namespace NeZoviReg.WebApi.Model.User;

public record BulkAddUsersRequest(List<BulkUser> Users);