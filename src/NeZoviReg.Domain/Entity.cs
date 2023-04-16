#pragma warning disable CS8618
namespace NeZoviReg.Domain;

/// <summary>
/// Base aggregate root.
/// Abstract class, defines for inheritance.
/// </summary>
public abstract class Entity : IEntity, IAuditingEntity
{
    protected Entity()
    {
        CreatedOn = DateTime.Now;
        CreatedBy = "Admin";
    }

    public int Id { get; protected set; }

    public string CreatedBy { get; protected set; }

    public string? ModifiedBy { get; protected set; }

    public DateTime CreatedOn { get; protected set; }

    public DateTime? ModifiedOn { get; protected set; }

    public byte[] Rowversion { get; protected set; }

    public IEntity AddIdentity(int id)
    {
        Id = id;

        return this;
    }

    public IAuditingEntity AddAuditing(IAuditingEntity entity, string user)
    {
        ModifiedOn = DateTime.Now;
        ModifiedBy = user;

        return this;
    }
}