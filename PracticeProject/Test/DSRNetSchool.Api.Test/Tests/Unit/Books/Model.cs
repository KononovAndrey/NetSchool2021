namespace DSRNetSchool.Api.Test.Tests.Unit.Books;

using DSRNetSchool.API.Controllers.Books.Models;

public partial class BookUnitTest
{
    public static UpdateBookModel UpdateBookModel(int authorId, string note, string title)
    {
        return new UpdateBookModel()
        {
            AuthorId = authorId,
            Note = note,
            Title = title
        };
    }
}
