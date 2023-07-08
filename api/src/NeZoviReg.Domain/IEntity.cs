namespace NeZoviReg.Domain;

/// <summary>
/// Defines aggregate root
/// </summary>
public interface IEntity : IAuditableEntity
{
    int Id { get; }

    /*byte[]*/DateTime Rowversion { get; }

    bool New => CreatedOn == default;
}