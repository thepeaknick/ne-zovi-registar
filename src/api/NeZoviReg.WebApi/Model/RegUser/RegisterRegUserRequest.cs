using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.WebApi.Model.RegUser;

public record RegisterRegUserRequest(string Email, string UserName, string Password, string FirstName, string LastName, RoleType[] Roles, string? Thumbprint = null);