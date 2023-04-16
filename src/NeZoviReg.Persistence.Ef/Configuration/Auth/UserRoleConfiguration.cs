using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Auth.Model;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        throw new NotImplementedException();
    }
}