using Microsoft.EntityFrameworkCore;

namespace NeZoviReg.Persistence.Ef;

public sealed class SqlLiteDbContext : DbContext
{
    private static bool _created;

    public SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options)
        : base(options)
    {
        if (_created)
            return;

        _created = true;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlLiteDbContext).Assembly);
    }
}