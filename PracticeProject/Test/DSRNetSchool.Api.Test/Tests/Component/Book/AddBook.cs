namespace DSRNetSchool.Api.Test.Tests.Component.Book;

using DSRNetSchool.Api.Test.Common.Extensions;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

public partial class BookIntegrationTest
{

    [Test]
    public async Task AddBook_ValidParameters_Authenticated_OkResponse()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var authorId = await GetExistedAuthorId();
        var request = AddBookRequest(authorId, Generator.ValidTitles.First(), Generator.ValidDescriptions.First());
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


        await using var context = await DbContext();
        var newBook = context.Books.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault();
        Assert.IsNotNull(newBook);

        Assert.AreEqual(request.AuthorId, newBook?.AuthorId);
        Assert.AreEqual(request.Title, newBook?.Title);
        Assert.AreEqual(request.Description, newBook?.Description);
    }

    [Test]
    [TestCaseSource(typeof(Generator), nameof(Generator.InvalidAuthorIds))]
    public async Task AddBook_InvalidAuthor_Authenticated_BadRequest(int authorId)
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var request = AddBookRequest(authorId, Generator.ValidTitles.First(), Generator.ValidDescriptions.First());
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task AddBook_ValidAuthor_Authenticated_OkResponse()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var authorId = await GetExistedAuthorId();
        var request = AddBookRequest(authorId, Generator.ValidTitles.First(), Generator.ValidDescriptions.First());
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    [TestCaseSource(typeof(Generator), nameof(Generator.InvalidTitles))]
    public async Task AddBook_InvalidTitle_Authenticated_BadRequest(string title)
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var authorId = await GetExistedAuthorId();
        var request = AddBookRequest(authorId, title, Generator.ValidDescriptions.First());
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    [TestCaseSource(typeof(Generator), nameof(Generator.ValidTitles))]
    public async Task AddBook_ValidTitle_Authenticated_OkResponse(string title)
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var authorId = await GetExistedAuthorId();
        var request = AddBookRequest(authorId, title, Generator.ValidDescriptions.First());
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    [TestCaseSource(typeof(Generator), nameof(Generator.InvalidDescriptions))]
    public async Task AddBook_InvalidDescription_Authenticated_BadRequest(string description)
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var authorId = await GetExistedAuthorId();
        var request = AddBookRequest(authorId, Generator.ValidTitles.First(), description);
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    [TestCaseSource(typeof(Generator), nameof(Generator.ValidDescriptions))]
    public async Task AddBook_ValidDescription_Authenticated_OkResponse(string description)
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var authorId = await GetExistedAuthorId();
        var request = AddBookRequest(authorId, Generator.ValidTitles.First(), description);
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public async Task AddBook_Unauthorized()
    {
        var url = Urls.AddBook;

        var authorId = await GetExistedAuthorId();
        var request = AddBookRequest(authorId, Generator.ValidTitles.First(), Generator.ValidDescriptions.First());
        var response = await apiClient.PostJson(url, request, null);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task AddBook_Forbidden()
    {
        var accessToken = await AuthenticateUser_EmptyScope();
        var url = Urls.AddBook;

        var authorId = await GetExistedAuthorId();
        var request = AddBookRequest(authorId, Generator.ValidTitles.First(), Generator.ValidDescriptions.First());
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }
}
