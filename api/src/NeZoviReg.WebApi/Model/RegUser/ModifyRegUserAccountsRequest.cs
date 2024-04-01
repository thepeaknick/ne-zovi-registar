using NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;

namespace NeZoviReg.WebApi.Model.RegUser;

/// <summary>
/// Zahtev za izmenu korisničkih naloga korisnika registra.
/// </summary>
/// <param name="Accounts">Lista korisiničkih naloga</param>
public record ModifyRegUserAccountsRequest(List<UserAccountData> Accounts);