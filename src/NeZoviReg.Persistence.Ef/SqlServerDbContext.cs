using Microsoft.EntityFrameworkCore;

namespace NeZoviReg.Persistence.Ef;

public class SqlServerDbContext : DbContext
{
    protected SqlServerDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerDbContext).Assembly);
    }
}