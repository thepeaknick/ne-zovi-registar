using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.Configuration.Domain;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.ConfigureEntity();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(User.FirstNameMaxLength);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(User.LastNameMaxLength);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(User.PhoneNumberMaxLength);

        builder.Property(x => x.Jmbg)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.OperatorId)
            .IsRequired();
        
        builder.Property(x => x.Active)
            .HasDefaultValue(true);

        builder.HasOne(kp => kp.Operator)
            .WithMany()
            .HasForeignKey(kp => kp.OperatorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}