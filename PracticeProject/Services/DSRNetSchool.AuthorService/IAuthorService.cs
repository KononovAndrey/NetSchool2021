namespace DSRNetSchool.AuthorService;

using DSRNetSchool.API.Authors.Models;

public interface IAuthorService
{
    Task<IEnumerable<AuthorModel>> GetAuthors();
}