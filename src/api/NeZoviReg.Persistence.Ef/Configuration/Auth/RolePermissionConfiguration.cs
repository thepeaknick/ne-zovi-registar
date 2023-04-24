using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Auth.Enum;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermissions);

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.ConfigureEntity(false);

        builder.HasData(Create(RoleType.Admin, PermissionType.All),
                        Create(RoleType.Obveznik, PermissionType.Read),
                        Create(RoleType.Obveznik, PermissionType.Delete),
                        Create(RoleType.Trgovac, PermissionType.Read),
                        Create(RoleType.Trgovac, PermissionType.ReadAll),
                        Create(RoleType.Potrosac, PermissionType.Read));

    }


    private static RolePermission Create(RoleType role, PermissionType permission)
    {
        var rp = new RolePermission((int)role, (int)permission);
        rp.AddCreation("test");

        return rp;
    }
}