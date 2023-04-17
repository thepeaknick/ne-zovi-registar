namespace NeZoviReg.Domain;

/// <summary>
/// Defines aggregate root
/// </summary>
public interface IEntity : IAuditingEntity
{
    int Id { get; }

    byte[] Rowversion { get; }
}