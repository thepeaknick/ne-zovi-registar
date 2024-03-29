namespace NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

/// <summary>
/// Spisak korisničkih naloga korisnika registra.
/// </summary>
public sealed record RegUserAccountsDto
{


    /// <summary>
    /// Guid ID korisnika registra
    /// </summary>
    public required Guid GuidId { get; init; }


    /// <summary>
    /// Lista korisničkih naloga
    /// </summary>
    public required List<UserAccountDto> Accounts { get; init; }
}