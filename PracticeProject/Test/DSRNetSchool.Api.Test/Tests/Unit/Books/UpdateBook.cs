namespace DSRNetSchool.Api.Test.Tests.Unit.Books;
using DSRNetSchool.BookService;
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public partial class BookUnitTest
{
    [Test]
    public async Task UpdateBook_ValidParameters_Success()
    {
        var bookService = services.Get<IBookService>();

        var bookId = await GetExistedBookId();
        var authorId = await GetExistedAuthorId();

        var model = UpdateBookModel(authorId, "test", "test");

        await bookService.UpdateBook(bookId, model);

        var resultBook = await GetBookById(bookId);

        Assert.IsNotNull(resultBook);

        Assert.AreEqual(model.AuthorId, resultBook.AuthorId);
        Assert.AreEqual(model.Title, resultBook.Title);
        Assert.AreEqual(model.Note, resultBook.Description);
    }
}
