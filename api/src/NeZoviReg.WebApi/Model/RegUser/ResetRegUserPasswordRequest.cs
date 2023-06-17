namespace NeZoviReg.WebApi.Model.RegUser;

/// <summary>
/// Zahtev za reset zaboravljene lozinke korisniak registra
/// </summary>
/// <param name="Email">Email korisnika registra</param>
/// <param name="Token">Pristupni token</param>
/// <param name="Password">Nova lozinka</param>
public record ResetRegUserPasswordRequest(string Email, string Token, string Password);