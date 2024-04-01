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

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(RegUser.EmailMaxLength);

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

        builder.HasIndex(x => x.CompanyName).IsUnique();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasIndex(x => x.RegNumber).IsUnique();

        builder.HasIndex(x => x.TaxNumber).IsUnique();

        builder.Navigation(n => n.RegUserRoles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(n => n.UserAccounts)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.UserAccounts)
            .WithOne(x => x.RegUser)
            .HasForeignKey(x => x.RegUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(Create(1, "RATEL", "mail@mail.com", "Palmotićeva 2", "00000000", "000000000", "Ime", "Prezime"));

        builder.HasData(Create(2, "RATEL2", "mail2@mail.com", "Palmotićeva 2", "11111111", "111111111", "Ime",
            "Prezime"));
    }

    private static RegUser Create(int id, string companyName, string email, string address, string regNumber,
        string taxNumber, string firstName, string lastName)
    {
        return RegUser.Create(id, companyName)
            .WithAddress(address)
            .WithEmail(email)
            .WithRegNumber(regNumber)
            .WithTaxNumber(taxNumber)
            .WithName(firstName, lastName);
    }
}