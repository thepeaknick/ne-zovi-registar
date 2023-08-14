namespace NeZoviReg.Abstractions.Messaging.Domain.Model.User;

/// <summary>
///  Informacije o registrovanom broju telefona.
/// </summary>
public sealed record UserInfoDto
{
    /// <summary>
    /// Broj telefona
    /// </summary>
    public required string PhoneNumber
    {
        get;
        init;
    }
    
    /// <summary>
    /// Postoji u registru
    /// </summary>
    public required bool Active
    {
        get;
        init;
    }
    
    /// <summary>
    /// Datum registracije
    /// </summary>
    public required DateTime RegisteredOn
    {
        get;
        init;
    }
    
    /// <summary>
    /// Datum brisanja iz registra
    /// </summary>
    public required DateTime? RemovedOn
    {
        get;
        init;
    }
    
    /// <summary>
    /// Naziv operatora
    /// </summary>
    public required string Operator
    {
        get;
        init;
    }
    
    /// <summary>
    /// Guid operatora
    /// </summary>
    public required Guid OperatorGuid
    {
        get;
        init;
    }
}