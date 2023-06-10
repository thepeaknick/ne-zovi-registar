using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.WebApi.Model.RegUser;

public record ModifyRegUserRequest(string? Name = default,
    string? Email = default,
    string? Address = default,
    string? RegNumber = default,
    string? TaxNumber = default,
    string? FirstName = default,
    string? LastName = default,
    string? UserName = default,
    List<RoleType>? Roles = default);