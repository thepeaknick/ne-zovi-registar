namespace NeZoviReg.Domain;

/// <summary>
/// Defines aggregate root
/// </summary>
public interface IEntity : IAuditableEntity
{
    int Id { get; }

    byte[] Rowversion { get; }
}