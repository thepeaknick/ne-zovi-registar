using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.WebApi.Model.RegUser;

public record RegisterRegUserRequest(string Name,
    string Email,
    string Address,
    string RegNumber,
    string TaxNumber,
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    RoleType[] Roles);