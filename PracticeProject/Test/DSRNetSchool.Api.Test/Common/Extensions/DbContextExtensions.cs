namespace DSRNetSchool.Api.Test.Common.Extensions;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public static class DbContextExtensions
{
    public static async Task Truncate<T>(this DbContext context, DbSet<T> dbSet) where T : class
    {
        await context.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {dbSet.EntityType.GetTableName()}");
    }
}
