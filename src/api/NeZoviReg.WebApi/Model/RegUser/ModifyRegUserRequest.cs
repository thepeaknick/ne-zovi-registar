using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.WebApi.Model.RegUser;

public record ModifyRegUserRequest(string? Email = default, string? UserName = default, string? Password = default, string? FirstName = default, string? LastName = default, List<RoleType>? Roles = default);