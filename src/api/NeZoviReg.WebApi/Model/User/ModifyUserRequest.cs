namespace NeZoviReg.WebApi.Model.User;

public record ModifyUserRequest(string? FirstName = null, string? LastName = null, string? Jmbg = null, string? PhoneNumber = null);