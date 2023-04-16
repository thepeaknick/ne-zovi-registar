using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeZoviReg.Domain.Model;

namespace NeZoviReg.Persistence.Ef.Configuration;

public class RegUserConfiguration : IEntityTypeConfiguration<RegUser>
{
    public void Configure(EntityTypeBuilder<RegUser> builder)
    {
        throw new NotImplementedException();
    }
}