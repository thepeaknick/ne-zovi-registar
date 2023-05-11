using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.WebApi.Model.RegUser;

public record ModifyRegUserRequest(string? Name = default,
    string? Address = default,
    string? RegNumber = default,
    string? TaxNumber = default,
    string? UserName = default,
    string? Password = default,
    List<RoleType>? Roles = default);