namespace DSRNetSchool.BookService;

using DSRNetSchool.API.Controllers.Books.Models;

public class BookService : IBookService
{
    public async Task<IEnumerable<BookModel>> GetBooks()
    {
        return new List<BookModel>()
        {
            new BookModel() { Title = "Title 1", Note = "Note note note"},
            new BookModel() { Title = "Title 2", Note = ""},
        };
    }

    public async Task<BookModel> GetBook(int id)
    {
        return new BookModel() { Title = "Title 1", Note = "Note note note" };
    }

    public async Task<BookModel> AddBook(AddBookModel model)
    {
        return new BookModel() { Title = "Title 1", Note = "Note note note" };
    }

    public async Task UpdateBook(int id, UpdateBookModel model)
    {

    }
}
