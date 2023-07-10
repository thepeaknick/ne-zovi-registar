namespace NeZoviReg.Domain;

/// <summary>
/// Defines aggregate root
/// </summary>
public interface IEntity : IAuditableEntity
{
    int Id { get; }

    //byte[] for SQL SERVER
    /*byte[]*/DateTime Rowversion { get; }

    bool New => CreatedOn == default;
}