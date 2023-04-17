using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model;

namespace NeZoviReg.Persistence.Ef.Configuration.Domain;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.ConfigureEntity();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(x => x.Jmbg)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(Create("Marko", "Bubulj", "1234", "012345567"));
    }

    private static User Create(string firstName, string lastName, string phone, string jmbg)
    {
        var user = new User(1, firstName, lastName, phone, jmbg);
        user.AddCreation("test");

        return user;
    }
}