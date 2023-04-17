using Microsoft.EntityFrameworkCore;

namespace NeZoviReg.Persistence.Ef;

public class SqlLiteDbContext : DbContext
{
    private static bool _created;

    protected SqlLiteDbContext(DbContextOptions options)
        : base(options)
    {
        if (_created)
            return;

        _created = true;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

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