using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Auth.Enum;
using NeZoviReg.Domain.Auth;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.ConfigureEntity();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(Create());
    }

    private static List<Permission> Create()
    {
        var permissions = Enum.GetValues<PermissionType>();

        return permissions.Select(r =>
        {
            var permission = new Permission((int) r, r.ToString());
            permission.AddCreation("test");

            return permission;
        }).ToList();
    }
}