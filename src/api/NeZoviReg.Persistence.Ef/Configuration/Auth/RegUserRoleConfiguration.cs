using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Auth.Enum;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class RegUserRoleConfiguration : IEntityTypeConfiguration<RegUserRole>
{
    public void Configure(EntityTypeBuilder<RegUserRole> builder)
    {
        builder.ToTable(TableNames.RegUsersRoles);

        builder.HasKey(x => new { x.RegUserId, x.RoleId });

        builder.ConfigureEntity(false);

        builder.HasOne(sc => sc.RegUser)
            .WithMany(s => s.RegUserRoles)
            .HasForeignKey(sc => sc.RegUserId);


        builder.HasOne(sc => sc.Role)
            .WithMany(s => s.RegUserRoles)
            .HasForeignKey(sc => sc.RoleId);

        builder.HasData(Create(1, RoleType.Admin));

    }


    private static RegUserRole Create(int regUserId, RoleType role)
    {
        var rr = new RegUserRole(regUserId, (int)role);
        rr.AddCreation("test");

        return rr;
    }
}