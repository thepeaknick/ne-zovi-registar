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

        builder.Property(x => x.CompanyName)
            .IsRequired()
            .HasMaxLength(RegUser.CompanyNameMaxLength);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(RegUser.AddressMaxLength);

        builder.Property(x => x.RegNumber)
            .IsRequired()
            .HasMaxLength(RegUser.RegNumberMaxLength);

        builder.Property(x => x.TaxNumber)
            .IsRequired()
            .HasMaxLength(RegUser.TaxNumberMaxLength);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(RegUser.FirstNameMaxLength);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(RegUser.LastNameMaxLength);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(RegUser.UsernameMaxLength);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(RegUser.PasswordMaxLength);

        builder.Property(x => x.RefreshToken);

        builder.Property(x => x.RefreshTokenExpirationTime);

        builder.HasIndex(x => x.CompanyName).IsUnique();

        builder.HasIndex(x => x.RegNumber).IsUnique();

        builder.HasIndex(x => x.TaxNumber).IsUnique();

        builder.HasIndex(x => x.Username).IsUnique();

        builder.Navigation(n => n.RegUserRoles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasData(Create("RATEL", "Palmotićeva 2", "17606590", "103986571", "Ime", "Prezime", "ratel", "test123"));

    }

    private static RegUser Create(string companyName, string address, string regNumber, string taxNumber, string firstName, string lastName, string userName, string password)
    {
        var regUser = new RegUser(1, companyName, userName)
            .WithAddress(address)
            .WithRegNumber(regNumber)
            .WithTaxNumber(taxNumber)
            .WithName(firstName, lastName)
            .WithPassword(password);

        regUser.AddCreation();

        return regUser;
    }
}