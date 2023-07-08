using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618
namespace NeZoviReg.Domain;

/// <summary>
/// Base entity.
/// Abstract class, defines for inheritance.
/// </summary>
public abstract class Entity : IEntity, IEquatable<Entity>
{
    protected Entity()
    {
    }

    protected Entity(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }

    public string CreatedBy { get; private set; } = string.Empty;

    public string? ModifiedBy { get; private set; }

    public DateTime CreatedOn { get; protected set; }

    public DateTime? ModifiedOn { get; private set; }

    public bool Deleted { get; private set; }

    [Timestamp]
    public /*byte[]*/DateTime Rowversion { get; private set; }

    public void AddCreation(string user = "init")
    {
        CreatedOn = DateTime.Now;
        CreatedBy = user;
    }

    public void AddModification(string user = "init")
    {
        ModifiedOn = DateTime.Now;
        ModifiedBy = user;
    }

    public void DeleteMe()
    {
        Deleted = true;
    }

    public static bool operator ==(Entity? first, Entity? second) =>
        first is not null && second is not null && first.Equals(second);

    public static bool operator !=(Entity? first, Entity? second) =>
        !(first == second);

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;
}