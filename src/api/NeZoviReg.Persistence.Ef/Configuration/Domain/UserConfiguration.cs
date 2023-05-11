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
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(x => x.Jmbg)
            .IsRequired()
            .HasMaxLength(13);

        builder.Property(x => x.OperatorId)
            .IsRequired()
            .HasMaxLength(25);

        builder.HasOne(kp => kp.Operator)
            .WithMany()
            .HasForeignKey(kp => kp.OperatorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}