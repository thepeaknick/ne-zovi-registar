using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NeZoviReg.Migrations.Ef.Extensions;

public static class MigrationExtensions
{
    public static MigrationBuilder RunFile(this MigrationBuilder builder,
        string filename,
        bool suppressTransaction = false,
        string? searchString = default,
        string? replaceWith = default)
    {
        if (searchString != default && replaceWith != default)
        {
            return builder.RunFile(filename, suppressTransaction, new ReplacePair(searchString, replaceWith));
        }

        return builder.RunFile(filename, suppressTransaction, default);
    }

    public static MigrationBuilder RunFile(this MigrationBuilder builder,
        string filename,
        bool suppressTransaction = false,
        params ReplacePair[]? parameters)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        string sqlPath = Path.IsPathRooted(filename) ? filename : Path.Combine(AppContext.BaseDirectory, "sql", filename);

        Console.WriteLine($"Excuting sql file BaseFolder:{AppContext.BaseDirectory},FullFile:{sqlPath}");

        if (!File.Exists(sqlPath))
        {
            throw new ArgumentException($"Migration .sql file not found: {filename}");
        }

        StringBuilder tmp = new StringBuilder(File.ReadAllText(sqlPath));

        if (parameters != null)
        {
#pragma warning disable CA1307 // Specify StringComparison
            foreach (ReplacePair pair in parameters)
            {
                if (!string.IsNullOrEmpty(pair.SearchString))
                {
                    tmp = tmp.Replace(pair.SearchString, pair.ReplaceString);
                }
            }
#pragma warning restore CA1307 // Specify StringComparison
        }

        builder.Sql(tmp.ToString(), suppressTransaction);

        return builder;
    }
}