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

        builder.HasData(Create(RoleType.Admin, PermissionType.RegUsersOnly),
                        Create(RoleType.Obveznik, PermissionType.Write),
                        Create(RoleType.Obveznik, PermissionType.Delete),
                        Create(RoleType.Obveznik, PermissionType.Read),
                        Create(RoleType.Trgovac, PermissionType.Read));

    }


    private static RolePermission Create(RoleType role, PermissionType permission)
    {
        return RolePermission.Create((int)role, (int)permission);
    }
}