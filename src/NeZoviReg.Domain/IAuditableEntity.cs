namespace NeZoviReg.Domain;

/// <summary>
/// Defines an auditing options
/// </summary>
public interface IAuditingEntity
{
    string CreatedBy { get; }

    string? ModifiedBy { get; }

    DateTime CreatedOn { get; }

    DateTime? ModifiedOn { get; }

    IAuditingEntity AddCreation(IAuditingEntity entity, string user);

    IAuditingEntity AddModification(IAuditingEntity entity, string user);
}