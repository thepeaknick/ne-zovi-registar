using Microsoft.EntityFrameworkCore;

namespace NeZoviReg.Persistence.Ef;

public class NeZoviRegDataContext : DbContext
{
    protected NeZoviRegDataContext(DbContextOptions options)
        : base(options)
    {
    }

    public NeZoviRegDataContext(DbContextOptions<NeZoviRegDataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NeZoviRegDataContext).Assembly);
    }
}