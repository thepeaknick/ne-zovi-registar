using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Auth;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Users)
            .WithMany();

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasData(Create());

        builder.Navigation(n => n.Permissions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(n => n.Users)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

    }

    private static List<Role> Create()
    {
        var roles = Role.GetValues();

        for (int i = 0; i < roles.Count; i++)
        {
            var role = roles[i];
            role.AddIdentity(++i);
        }

        return roles.Select(r => new Role(r.Id, r.Value, r.Name)).ToList();
    }
}