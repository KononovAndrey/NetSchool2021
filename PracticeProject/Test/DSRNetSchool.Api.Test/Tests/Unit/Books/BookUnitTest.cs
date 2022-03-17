namespace DSRNetSchool.Api.Test.Tests.Unit.Books;

using DSRNetSchool.Api.Test.Common;
using DSRNetSchool.Db.Entities;
using System.Linq;
using System.Threading.Tasks;

public partial class BookUnitTest : UnitTest
{
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

    public async Task<int> GetExistedBookId()
    {
        await using var context = await DbContext();
        if (context.Books.Count() == 0)
        {
            Book book = new Book()
            {
                AuthorId = await GetExistedAuthorId(),
                Title = "Test",
                Description = "Test"
            };
            context.Books.Add(book);
            context.SaveChanges();
        }

        await using var context1 = await DbContext();
        var book1 = context1.Books.AsEnumerable().First();
        return book1.Id;
    }


    public async Task<Book> GetBookById(int id)
    {
        await using var context = await DbContext();
        var book = context.Books.FirstOrDefault(x => x.Id == id);
        return book;
    }

    protected async override Task ClearDb()
    {
        await using var context = await DbContext();
        context.Books.RemoveRange(context.Books);
        context.Authors.RemoveRange(context.Authors);
        context.SaveChanges();
    }
}
