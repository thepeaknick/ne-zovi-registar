#pragma warning disable CS8618
namespace NeZoviReg.Domain;

/// <summary>
/// Base aggregate root.
/// Abstract class, defines for inheritance.
/// </summary>
public abstract class Entity : IEntity
{
    protected Entity()
    {
    }

    protected Entity(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }

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