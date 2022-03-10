namespace DSRNetSchool.API.Controllers.Authors.Models;

using AutoMapper;
using DSRNetSchool.API.Authors.Models;

public class AuthorResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class AuthorResponseProfile : Profile
{
    public AuthorResponseProfile()
    {
        CreateMap<AuthorModel, AuthorResponse>();
    }
}
