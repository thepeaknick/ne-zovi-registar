#pragma warning disable CS8618
using NeZoviReg.Domain.Abstractions;

namespace NeZoviReg.Domain;

/// <summary>
/// Base aggregate root.
/// Abstract class, defines for inheritance.
/// </summary>
public abstract class EnumerationEntity : Enumeration<EnumerationEntity>, IEntity
{
    protected EnumerationEntity()
    {
    }

    protected EnumerationEntity(int id, string name):
        base(id, name)
    {
    }

    public string CreatedBy { get; private set; }

    public string? ModifiedBy { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public byte[] Rowversion { get; private set; }

    public void AddCreation(string user)
    {
        CreatedOn = DateTime.Now;
        CreatedBy = user;
    }

    public void AddModification(string user)
    {
        ModifiedOn = DateTime.Now;
        ModifiedBy = user;
    }
}