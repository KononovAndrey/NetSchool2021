namespace DSRNetSchool.API.Controllers.Books.Models;

using AutoMapper;
using DSRNetSchool.Db.Entities;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}

public class BookModelProfile : Profile
{
    public BookModelProfile()
    {
        CreateMap<Book, BookModel>();
    }
}
