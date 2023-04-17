using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NeZoviReg.Persistence.Ef;

namespace NeZoviReg.Migrations.Ef;

public class NeZoviRegDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
{
    public SqlServerDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("SqlServerDatabase");

        var builder = new DbContextOptionsBuilder<SqlServerDbContext>()
            .UseSqlServer(connectionString, o =>
            {
                o.MigrationsAssembly("NeZoviReg.Migrations.Ef");
            });
        
        return new SqlServerDbContext(builder.Options);
    }
}