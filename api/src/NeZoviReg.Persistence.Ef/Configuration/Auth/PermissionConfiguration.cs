using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.ConfigureEnumerationEntity();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(Create());
    }

    private static List<Permission> Create()
    {
        var permissions = Permission.GetValues();

        return permissions.Select(r =>
        {
            var permission = new Permission(r.Id, r.Name);
            permission.AddCreation();
            return permission;
        }).ToList();
    }
}