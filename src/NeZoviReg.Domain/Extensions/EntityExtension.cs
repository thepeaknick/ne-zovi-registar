namespace NeZoviReg.Domain.Extensions;

public static class EntityExtension
{
    public static T AddCreated<T>(this T entity, string user)
        where T : IAuditingEntity
    {
        entity.AddCreation(user);

        return entity;
    }

    public static T AddModified<T>(this T entity, string user)
        where T : IAuditingEntity
    {
        entity.AddModification(user);

        return entity;
    }
}