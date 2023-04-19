using NeZoviReg.Migrations.Ef;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

try
{
    Console.WriteLine("-->Starting migration!");
    var sw = Stopwatch.StartNew();

    var f = new NeZoviRegDbContextFactory();
    using (var context = f.CreateDbContext(args))
    {
        context.Database.Migrate();
    }

    Console.WriteLine($"<--Migration finished! {sw.ElapsedMilliseconds}ms");
}
catch (Exception e)
{
    Console.WriteLine(e);
}