using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Auth.Model;
using NeZoviReg.Auth.Model.Enum;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(Create());
    }

    private static List<Permission> Create()
    {
        var roles = Permission.GetValues();

        for (int i = 0; i < roles.Count; i++)
        {
            var role = roles[i];
            role.AddIdentity(++i);
        }

        return roles.Select(r => new Permission(r.Id, r.Value, r.Name)).ToList();
    }
}