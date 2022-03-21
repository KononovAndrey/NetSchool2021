namespace DSRNetSchool.Api.Test.Tests.Component.Book;

using DSRNetSchool.Api.Test.Common;
using DSRNetSchool.Db.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public partial class BookIntegrationTest : ComponentTest
{
    [SetUp]
    public async Task SetUp()
    {
        await using var context = await DbContext();

        context.Books.RemoveRange(context.Books);
        context.Authors.RemoveRange(context.Authors);
        context.Categories.RemoveRange(context.Categories);
        context.SaveChanges();

        var a1 = new Db.Entities.Author()
        {
            Name = "Mark Twen",
            Detail = new Db.Entities.AuthorDetail()
            {
                Country = "USA",
                Family = "",
            }
        };
        context.Authors.Add(a1);

        var a2 = new Db.Entities.Author()
        {
            Name = "Lev Tolstoy",
            Detail = new Db.Entities.AuthorDetail()
            {
                Country = "Russia",
                Family = "",
            }
        };
        context.Authors.Add(a2);

        var c1 = new Db.Entities.Category()
        {
            Title = "Classic"
        };
        context.Categories.Add(c1);

        context.Books.Add(new Db.Entities.Book()
        {
            Title = "Tom Soyer",
            Description = "description description description description ",
            Author = a1,
            Categories = new List<Db.Entities.Category>() { c1 },
        });

        context.Books.Add(new Db.Entities.Book()
        {
            Title = "War and peace",
            Description = "description description description description ",
            Author = a2,
            Categories = new List<Db.Entities.Category>() { c1 },
        });

        context.SaveChanges();
    }

    [TearDown]
    public async override Task TearDown()
    {
        await using var context = await DbContext();
        context.Authors.RemoveRange(context.Authors);
        context.Categories.RemoveRange(context.Categories);
        context.Books.RemoveRange(context.Books);
        context.SaveChanges();
        await base.TearDown();
    }

    protected static class Urls
    {
        public static string GetBooks(int? offset = null, int? limit = null)
        {

            if (offset is null && limit is null)
                return $"/api/v1/books";
            List<string> queryParameters = new List<string>();

            if (offset.HasValue)
            {
                queryParameters.Add($"offset={offset}");
            }

            if (limit.HasValue)
            {
                queryParameters.Add($"limit={limit}");
            }

            var queryString = string.Join("&", queryParameters);
            return $"/api/v1/books?{queryString}";
        }

        public static string GetBook(string id) => $"/api/v1/books/{id}";

        public static string DeleteBook(string id) => $"/api/v1/books/{id}";

        public static string UpdateBook(string id) => $"/api/v1/books/{id}";

        public static string AddBook => $"/api/v1/books";
    }

    public static class Scopes
    {
        public static string ReadBooks => "offline_access books_read";

        public static string WriteBooks => "offline_access books_write";

        public static string ReadAndWriteBooks => "offline_access books_read books_write";

        public static string Empty => "offline_access";
    }

    public async Task<string> AuthenticateUser_ReadAndWriteBooksScope()
    {
        var user = GetTestUser();
        var tokenResponse = await AuthenticateTestUser(user.Username, user.Password, Scopes.ReadAndWriteBooks);
        return tokenResponse.AccessToken;
    }

    public async Task<string> AuthenticateUser_EmptyScope()
    {
        var user = GetTestUser();
        var tokenResponse = await AuthenticateTestUser(user.Username, user.Password, Scopes.Empty);
        return tokenResponse.AccessToken;
    }

    public async Task<int> GetExistedAuthorId()
    {
        await using var context = await DbContext();
        if (context.Authors.Count() == 0)
        {
            Author author1 = new Author()
            {
                Name = "Test",
                Detail = new Db.Entities.AuthorDetail()
                {
                    Country = "Test",
                    Family = "Test",
                }
            };
            context.Authors.Add(author1);
            context.SaveChanges();
        }

        await using var context1 = await DbContext();
        var author = context1.Authors.AsEnumerable().First();
        return author.Id;
    }

    public async Task<int> GetNotExistedAuthorId()
    {
        await using var context = await DbContext();
        var maxExistedAuthorId = context.Authors.Max(x => x.Id);

        return maxExistedAuthorId + 1;
    }

}
