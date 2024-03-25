using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable(TableNames.UserAccounts);

        builder.ConfigureEntity();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(RegUser.UsernameMaxLength);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(RegUser.PasswordMaxLength);
        
        builder.Property(x => x.RegUserId)
            .IsRequired();

        builder.Property(x => x.AccessTokenExpirationTime);
        
        builder.Property(x => x.RefreshToken);

        builder.Property(x => x.RefreshTokenExpirationTime);
        
        builder.Property(x => x.ForgotPasswordToken);

        builder.Property(x => x.ForgotPasswordTokenExpirationTime);

        builder.HasIndex(x => x.Username).IsUnique();
    }
}