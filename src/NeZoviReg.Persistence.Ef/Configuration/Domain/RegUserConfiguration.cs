using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.Configuration.Domain;

public class RegUserConfiguration : IEntityTypeConfiguration<RegUser>
{
    public void Configure(EntityTypeBuilder<RegUser> builder)
    {
        builder.ToTable(TableNames.RegUsers);

        builder.ConfigureEntity();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ThumbPrint)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Email).IsUnique();

        /*builder.HasMany(x => x.Roles)
            .WithMany(x=>x.Users)
            .UsingEntity(j => j
                .ToTable("RegUserRole")
                .HasData(new { RolesId = 1, UsersId = 1 }
                ));*/

        builder.Navigation(n => n.Roles)
             .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasData(Create("Petar", "Petrovic", "petar.petrovic@mts.rs"));

    }

    private static RegUser Create(string firstName, string lastName, string email)
    {
        var regUser = new RegUser(1, firstName, lastName, email);
        regUser.AddCreation("test");
        return regUser;
    }
}