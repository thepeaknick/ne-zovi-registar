namespace NeZoviReg.WebApi.Model.RegUser;

/// <summary>
/// Zahtev za prijavu.
/// </summary>
/// <param name="Username">Korisničko ime</param>
/// <param name="Password">Lozinka</param>
public record LoginRequest(string Username, string Password);