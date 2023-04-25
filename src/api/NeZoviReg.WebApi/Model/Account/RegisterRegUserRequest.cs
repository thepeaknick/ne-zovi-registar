using NeZoviReg.Auth.Model.Enum;

namespace NeZoviReg.WebApi.Model.Account;

public record RegisterRegUserRequest(string Email, string UserName, string Password, string FirstName, string LastName);