using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class RegUserAccountConfiguration : IEntityTypeConfiguration<RegUserAccount>
{
    public void Configure(EntityTypeBuilder<RegUserAccount> builder)
    {
        builder.ToTable(TableNames.RegUserAccounts);

        builder.ConfigureEntity();

        builder.Property(x => x.GuidId)
            .IsRequired();
        
        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(RegUserAccount.UsernameMaxLength);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(RegUserAccount.PasswordMaxLength);
        
        builder.Property(x => x.RegUserId)
            .IsRequired();

        builder.Property(x => x.AccessTokenExpirationTime);
        
        builder.Property(x => x.RefreshToken);

        builder.Property(x => x.RefreshTokenExpirationTime);
        
        builder.Property(x => x.ForgotPasswordToken);

        builder.Property(x => x.ForgotPasswordTokenExpirationTime);

        builder.HasIndex(x => x.Username).IsUnique();
        
        builder.Property(x => x.FirstName)
            .IsRequired(false)
            .HasMaxLength(RegUserAccount.FirstNameMaxLength);

        builder.Property(x => x.LastName)
            .IsRequired(false)
            .HasMaxLength(RegUserAccount.LastNameMaxLength);
        
        builder.HasData(Create(1, 1, "ratel", "test123", "admin", "ratel"));
        builder.HasData(Create(2, 2, "ratel2", "test123", "admin", "ratel"));
    }
    
    private static RegUserAccount Create(int id, int regUserId, string userName, string password, string firstName, string lastName)
    {
        return RegUserAccount.Create(id, userName, password)
            .WithRegUserId(regUserId)
            .WithFirstName(firstName)
            .WithLastName(lastName);
    }
}