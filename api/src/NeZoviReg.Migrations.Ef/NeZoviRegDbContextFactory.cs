using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NeZoviReg.Persistence.Ef;

namespace NeZoviReg.Migrations.Ef;

public class NeZoviRegDbContextFactory : IDesignTimeDbContextFactory<NeZoviRegDataContext>
{
    public NeZoviRegDataContext CreateDbContext(string[] args)
    {
        var envName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        Console.WriteLine($"Running in the ENVIRONMENT={envName}");
        
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional:false, true)
            .AddJsonFile($"appsettings.{envName}.json", optional:true, true)
            .Build();

        var connectionString = configuration.GetConnectionString("SqlServerDatabase");

        var builder = new DbContextOptionsBuilder<NeZoviRegDataContext>()
            .UseSqlServer(connectionString, o =>
            {
                o.MigrationsAssembly("NeZoviReg.Migrations.Ef");
            });

        return new NeZoviRegDataContext(builder.Options);
    }
}