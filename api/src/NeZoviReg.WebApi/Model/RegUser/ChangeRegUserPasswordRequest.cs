namespace NeZoviReg.WebApi.Model.RegUser;

/// <summary>
/// Zahtev za izmenu lozinke.
/// </summary>
/// <param name="Password">Stara lozinka.</param>
/// <param name="NewPassword">Nova lozinka.</param>
public record ChangeRegUserPasswordRequest(string Password, string NewPassword);