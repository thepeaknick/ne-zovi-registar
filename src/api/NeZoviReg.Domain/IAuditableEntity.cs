namespace NeZoviReg.Domain;

/// <summary>
/// Defines an auditing options
/// </summary>
public interface IAuditableEntity
{
    string CreatedBy { get; }

    string? ModifiedBy { get; }

    DateTime CreatedOn { get; }

    DateTime? ModifiedOn { get; }

    void AddCreation(string user);

    void AddModification(string user);
}