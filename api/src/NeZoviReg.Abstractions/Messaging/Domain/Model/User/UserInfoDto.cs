namespace NeZoviReg.Abstractions.Messaging.Domain.Model.User;

/// <summary>
///  Informacije o registrovanom broju telefona.
/// </summary>
/// <param name="PhoneNumber"> Broj telefona</param>
/// <param name="Active">Postoji u registru</param>
/// <param name="RegisteredOn">Datum registracije</param>
/// <param name="RemovedOn">Datum brisanja iz registra</param>
/// <param name="Operator">Naziv operatora</param>
public sealed record UserInfoDto
{
    public required string PhoneNumber
    {
        get;
        init;
    }
    
    public required bool Active
    {
        get;
        init;
    }
    
    public required DateTime RegisteredOn
    {
        get;
        init;
    }
    
    public required DateTime? RemovedOn
    {
        get;
        init;
    }
    
    public required string Operator
    {
        get;
        init;
    }
}