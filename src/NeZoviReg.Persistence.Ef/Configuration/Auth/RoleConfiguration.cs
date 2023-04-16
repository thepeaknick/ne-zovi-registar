using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Auth.Model;

namespace NeZoviReg.Persistence.Ef.Configuration.Auth;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        throw new NotImplementedException();
    }
}