namespace DSRNetSchool.BookService;

using DSRNetSchool.API.Controllers.Books.Models;

public interface IBookService
{
    Task<IEnumerable<BookModel>> GetBooks();
    Task<BookModel> GetBook(int id);
    Task<BookModel> AddBook(AddBookModel model);
    Task UpdateBook(int id, UpdateBookModel model);
}