namespace DSRNetSchool.API.Controllers.Books;

using AutoMapper;
using DSRNetSchool.API.Controllers.Books.Models;
using DSRNetSchool.BookService;
using DSRNetSchool.Common.Security;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AppScopes.BooksRead)]
    public async Task<IEnumerable<BookResponse>> GetBooks([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var books = await bookService.GetBooks(offset, limit);
        var response = mapper.Map<IEnumerable<BookResponse>>(books);

        return response;
    }

    [HttpGet("{id}")]
    [Authorize(AppScopes.BooksRead)]
    public async Task<BookResponse> GetBookById([FromRoute] int id)
    {
        var book = await bookService.GetBook(id);
        var response = mapper.Map<BookResponse>(book);

        return response;
    }

    [HttpPost("")]
    [Authorize(AppScopes.BooksWrite)]
    public async Task<BookResponse> AddBook([FromBody] AddBookRequest request)
    {
        var model = mapper.Map<AddBookModel>(request);
        var book = await bookService.AddBook(model);
        var response = mapper.Map<BookResponse>(book);

        return response;
    }

    [HttpPut("{id}")]
    [Authorize(AppScopes.BooksWrite)]
    public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookRequest request)
    {
        var model = mapper.Map<UpdateBookModel>(request);
        await bookService.UpdateBook(id, model);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(AppScopes.BooksWrite)]
    public async Task<IActionResult> DeleteBook([FromRoute] int id)
    {
        await bookService.DeleteBook(id);

        return Ok();
    }
}
