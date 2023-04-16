#pragma warning disable CS8618
using NeZoviReg.Domain.Abstractions;

namespace NeZoviReg.Domain;

/// <summary>
/// Base aggregate root.
/// Abstract class, defines for inheritance.
/// </summary>
public abstract class EnumerationEntity : Enumeration<EnumerationEntity>, IEntity, IAuditingEntity
{
    protected EnumerationEntity(int id, string name):
        base(id, name)
    {
    }

    public string CreatedBy { get; private set; }

    public string? ModifiedBy { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public byte[] Rowversion { get; private set; }

    public IEntity AddIdentity(int id)
    {
        Id = id;

        return this;
    }

    public IAuditingEntity AddCreation(IAuditingEntity entity, string user)
    {
        CreatedOn = DateTime.Now;
        CreatedBy = user;

        return this;
    }

    public IAuditingEntity AddModification(IAuditingEntity entity, string user)
    {
        ModifiedOn = DateTime.Now;
        ModifiedBy = user;

        return this;
    }
}