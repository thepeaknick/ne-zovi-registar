using NeZoviReg.Abstractions.Shared.Model.Domain;

namespace NeZoviReg.WebApi.Model.User;

/// <summary>
/// Zahtev za bul registaciju brojeva telefona.
/// </summary>
/// <param name="Users">Lista korisnika</param>
public record BulkAddUsersRequest(List<BulkUser> Users);