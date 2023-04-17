// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

try
{
    /*throw new AccessViolationException("Use Package Manager Console tools for now. " + Environment.NewLine +
                                       "1. Add-Migration -StartupProject Idis.Dpn.Ef.Migrations -Project Idis.Dpn.Ef.Migrations InitMigration" + Environment.NewLine +
                                       "2. Update-Database -StartupProject Idis.Dpn.Ef.Migrations -Project Idis.Dpn.Ef.Migrations");*/

    Console.WriteLine("-->Staring migration!");
    Stopwatch sw = Stopwatch.StartNew();
    //DpnMigrationsDesignTimeContextFactory f = new DpnMigrationsDesignTimeContextFactory();

    if (args.Length <= 0)
    {
        Console.WriteLine("Migracije zagnane brez parametra. Uporabljen bo default connection.");
    }

    /*using (var context = f.CreateDbContext(args))
    {
        context.Database.Migrate();
    }*/

    Console.WriteLine($"<--Migration finished! {sw.ElapsedMilliseconds}ms");
}
catch (Exception e)
{
    Console.WriteLine(e);
}