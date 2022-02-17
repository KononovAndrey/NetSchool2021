namespace DSRNetSchool.API.Controllers.Books;

using AutoMapper;
using DSRNetSchool.API.Controllers.Books.Models;
using DSRNetSchool.BookService;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/books")]
[ApiController]
[ApiVersion("1.0")]
public class BooksController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<BooksController> logger;
    private readonly IBookService bookService;

    public BooksController(IMapper mapper, ILogger<BooksController> logger, IBookService bookService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.bookService = bookService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<BookResponse>> GetBooks()
    {
        var books = await bookService.GetBooks();
        var response = mapper.Map<IEnumerable<BookResponse>>(books);

        return response;
    }

    [HttpGet("{id}")]
    public async Task<BookResponse> GetBookById([FromRoute] int id)
    {
        var book = await bookService.GetBook(id);
        var response = mapper.Map<BookResponse>(book);

        return response;
    }

    [HttpPost("")]
    public async Task<BookResponse> AddBook([FromBody] AddBookRequest request)
    {
        var model = mapper.Map<AddBookModel>(request);
        var book = await bookService.AddBook(model);
        var response = mapper.Map<BookResponse>(book);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookRequest request)
    {
        var model = mapper.Map<UpdateBookModel>(request);
        await bookService.UpdateBook(id, model);

        return Ok();
    }
}
