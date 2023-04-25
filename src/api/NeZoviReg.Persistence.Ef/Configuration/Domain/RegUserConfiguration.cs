using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.Configuration.Domain;

public class RegUserConfiguration : IEntityTypeConfiguration<RegUser>
{
    public void Configure(EntityTypeBuilder<RegUser> builder)
    {
        builder.ToTable(TableNames.RegUsers);

        builder.ConfigureEntity();

        builder.Property(x => x.GuidId)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .IsRequired(false)
            .HasMaxLength(RegUser.FirstNameMaxLength);

        builder.Property(x => x.LastName)
            .IsRequired(false)
            .HasMaxLength(RegUser.LastNameMaxLength);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(RegUser.UsernameMaxLength);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(RegUser.PasswordMaxLength);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(RegUser.EmailMaxLength);

        builder.Property(x => x.ThumbPrint)
            .IsRequired(false)
            .HasMaxLength(RegUser.ThumbprintMaxLength);

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasIndex(x => x.Username).IsUnique();

        builder.Navigation(n => n.RegUserRoles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasData(Create("Petar", "Petrovic", "pPetrovic", "test123", "petar.petrovic@mts.rs"));

    }

    private static RegUser Create(string firstName, string lastName, string userName, string password, string email)
    {
        var regUser = new RegUser(1, userName, email)
            .AddFirstName(firstName)
            .AddLastName(lastName)
            .AddPassword(password);

        regUser.AddCreation("test");

        return regUser;
    }
}