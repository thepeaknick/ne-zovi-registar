namespace NeZoviReg.WebApi.Model.RegUser;

/// <summary>
/// Zahtev za slanje e-mejla.
/// </summary>
/// <param name="FirstName">Ime</param>
/// <param name="LastName">Prezime</param>
/// <param name="CompanyName">Ime kompanije</param>
/// <param name="EmailFrom">Email</param>
/// <param name="PhoneNumber">Broj telefona</param>
/// <param name="Content">Email sadrzaj</param>
public record SendEmailRequest(string? FirstName, string? LastName, string? CompanyName, string? EmailFrom, string? PhoneNumber, string? Content);