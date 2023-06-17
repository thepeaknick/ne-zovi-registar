namespace NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

/// <summary>
/// Role korisnika registra
/// </summary>
public enum RoleType
{
    //RATEL or partner
    /// <summary>
    /// Administrator
    /// </summary>
    Admin = 1,

    //Content provder
    /// <summary>
    /// Trgovac
    /// </summary>
    Trgovac,

    //Operater
    /// <summary>
    /// Obveznik
    /// </summary>
    Obveznik
}