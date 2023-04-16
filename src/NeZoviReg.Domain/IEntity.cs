namespace NeZoviReg.Domain;

/// <summary>
/// Defines aggregate root
/// </summary>
public interface IEntity
{
    int Id { get; }

    byte[] Rowversion { get; }

    IEntity AddIdentity(int id);
}