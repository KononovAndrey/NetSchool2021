namespace DSRNetSchool.Db.Context.Setup;

using DSRNetSchool.Db.Context.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DbSeed
{
    private static void AddBooks(MainDbContext context)
    {
        if (context.Books.Any() || context.Authors.Any() || context.Categories.Any())
            return;

        var a1 = new Entities.Author()
        {
            Name = "Mark Twen",
            Detail = new Entities.AuthorDetail()
            {
                Country = "USA",
                Family = "",
            }
        };
        context.Authors.Add(a1);

        var a2 = new Entities.Author()
        {
            Name = "Lev Tolstoy",
            Detail = new Entities.AuthorDetail()
            {
                Country = "Russia",
                Family = "",
            }
        };
        context.Authors.Add(a2);

        var c1 = new Entities.Category()
        {
            Title = "Classic"
        };
        context.Categories.Add(c1);

        context.Books.Add(new Entities.Book()
        {
            Title = "Tom Soyer",
            Description = "description description description description ",
            Author = a1,
            Categories = new List<Entities.Category>() { c1 },
        });

        context.Books.Add(new Entities.Book()
        {
            Title = "War and peace",
            Description = "description description description description ",
            Author = a2,
            Categories = new List<Entities.Category>() { c1 },
        });

        context.SaveChanges();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>();
        using var context = factory.CreateDbContext();

        AddBooks(context);
    }
}
