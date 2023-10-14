namespace NeZoviReg.WebApi.Model.User;

/// <summary>
/// Zahtev za bul de-registaciju brojeva telefona.
/// </summary>
/// <param name="PhoneNumbers">Brojevi telefona</param>
public record BulkRemoveUsersRequest(List<string> Users);