using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Auth.Enum;
using NeZoviReg.Domain.Auth;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.ConfigureEntity();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Users)
            .WithMany(x=>x.Roles);

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.Navigation(n => n.Permissions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(n => n.Users)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasData(Create());

    }

    private static List<Role> Create()
    {
        var roles = Enum.GetValues<RoleType>();

        return roles.Select(r =>
        {
            var role = new Role((int) r, r.ToString());
            role.AddCreation("test");

            return role;
        }).ToList();
    }
}