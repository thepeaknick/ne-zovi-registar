namespace NeZoviReg.WebApi.Model.RegUser;

public record SendEmailRequest(string? FirstName, string? LastName, string? CompanyName, string? EmailFrom, string? PhoneNumber, string? Content);