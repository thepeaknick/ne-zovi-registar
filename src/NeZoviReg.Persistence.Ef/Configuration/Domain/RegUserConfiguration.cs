using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model;

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

        builder.Navigation(n => n.Roles)
             .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasData(Create("Petar", "Petrovic", "markobubulj@mts.rs"));

    }

    private static RegUser Create(string firstName, string lastName, string email)
    {
        var rp = new RegUser(1, firstName, lastName, email);
        rp.AddCreation("test");

        return rp;
    }
}