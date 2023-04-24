using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.ConfigureEnumerationEntity();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Roles)
            .UsingEntity<RegUserRole>(); ;

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
        var roles = Role.GetValues();

        return roles.Select(r =>
        {
            var role = new Role(r.Id, r.Name);
            role.AddCreation("test");
            return role;
        }).ToList();
    }
}