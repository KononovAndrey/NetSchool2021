namespace DSRNetSchool.Api.Test.Tests.Component.Book;

using DSRNetSchool.API.Controllers.Books.Models;

public partial class BookIntegrationTest
{
    public static AddBookRequest AddBookRequest(int authorId, string title, string description)
    {
        return new AddBookRequest()
        {
            AuthorId = authorId,
            Title = title,
            Description = description
        };
    }

    public static UpdateBookRequest UpdateBookRequest(int authorId, string title, string description)
    {
        return new UpdateBookRequest()
        {
            AuthorId = authorId,
            Title = title,
            Description = description
        };
    }
}
