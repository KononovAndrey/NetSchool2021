namespace DSRNetSchool.Db.Context.Factories;

using DSRNetSchool.Db.Context.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.design.json")
             .Build();

        var options = new DbContextOptionsBuilder<MainDbContext>()
                      .UseSqlServer(configuration.GetConnectionString("MainDbContext"),
                            opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                       ).Options;

        return new MainDbContextFactory(options).Create();
    }
}
