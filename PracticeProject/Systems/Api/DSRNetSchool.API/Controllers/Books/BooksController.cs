namespace DSRNetSchool.API.Controllers.Books;

using DSRNetSchool.API.Controllers.Books.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> logger;

    public BooksController(ILogger<BooksController> logger)
    {
        this.logger = logger;
    }

    [HttpGet("info")]
    public IActionResult GetInfo()
    {
        return Ok(new {
            IAmOk = true,
        });
    }

    [HttpGet("")]
    [ApiVersion("1.0")]
    public List<Book> GetBooks()
    {
        return new List<Book>()
        {
            new Book() {
                Id = 1,
                Title = "T1",
                Description = "D1"
            },
            new Book() {
                Id = 2,
                Title = "T2",
                Description = "D2"
            },
        };
    }

    [HttpGet("")]
    [ApiVersion("2.0")]
    public List<BookV2> GetBooksV2()
    {
        return new List<BookV2>()
        {
            new BookV2() {
                Id = 1,
                Title = "T1",
                Author = "A1",
                Description = "D1"
            },
            new BookV2() {
                Id = 2,
                Title = "T2",
                Author = "A2",
                Description = "D2"
            },
        };
    }

}
