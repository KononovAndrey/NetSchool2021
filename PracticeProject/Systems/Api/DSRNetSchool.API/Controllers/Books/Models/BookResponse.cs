namespace DSRNetSchool.API.Controllers.Books.Models;

using AutoMapper;

public class BookResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class BookResponseProfile : Profile
{
    public BookResponseProfile()
    {
        CreateMap<BookModel, BookResponse>()
            .ForMember(d => d.Description, a => a.MapFrom(s => s.Note));
    }
}
