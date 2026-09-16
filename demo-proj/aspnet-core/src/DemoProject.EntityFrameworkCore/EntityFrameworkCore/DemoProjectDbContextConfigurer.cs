using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DemoProject.EntityFrameworkCore;

public static class DemoProjectDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<DemoProjectDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<DemoProjectDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
