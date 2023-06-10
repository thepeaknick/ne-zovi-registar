namespace NeZoviReg.WebApi.Model.RegUser;

public record ResetRegUserPasswordRequest(string Email, string Token, string Password);