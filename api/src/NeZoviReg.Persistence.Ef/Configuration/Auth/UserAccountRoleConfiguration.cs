using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Auth.Enum;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class UserAccountRoleConfiguration : IEntityTypeConfiguration<UserAccountRole>
{
    public void Configure(EntityTypeBuilder<UserAccountRole> builder)
    {
        builder.ToTable(TableNames.UserAccountsRoles);

        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.ConfigureEntity(false);

        builder.HasOne(sc => sc.Role)
            .WithMany(s => s.UserAccountRoles)
            .HasForeignKey(sc => sc.RoleId)
            .OnDelete(DeleteBehavior.ClientNoAction);

        builder.HasOne(sc => sc.UserAccount)
            .WithMany(s => s.UserAccountRoles)
            .HasForeignKey(sc => sc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(Create(1, RoleType.Admin));
        builder.HasData(Create(2, RoleType.Admin));

    }


    private static UserAccountRole Create(int userId, RoleType role)
    {
        return  UserAccountRole.Create(userId, (int)role);
    }
}