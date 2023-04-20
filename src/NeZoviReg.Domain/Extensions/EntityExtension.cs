namespace NeZoviReg.Domain.Extensions;

public static class EntityExtension
{
    public static T AddCreated<T>(this T entity, string user)
        where T : IAuditableEntity
    {
        entity.AddCreation(user);

        return entity;
    }

    public static T AddModified<T>(this T entity, string user)
        where T : IAuditableEntity
    {
        entity.AddModification(user);

        return entity;
    }
}