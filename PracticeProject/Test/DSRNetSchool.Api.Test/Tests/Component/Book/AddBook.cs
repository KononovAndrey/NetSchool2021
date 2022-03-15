namespace DSRNetSchool.Api.Test.Tests.Component.Book;

using DSRNetSchool.Api.Test.Common.Extensions;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

public partial class BookTest
{

    [Test]
    public async Task AddBook_ValidParameters_Authenticated_Success()
    {
        var testUser = GetTestUser();
        var tokenResponse = await AuthenticateTestUser(testUser.Username, testUser.Password, Scopes.WriteBooks);
        var accessToken = tokenResponse.AccessToken;
        var url = Urls.AddBook;

        await using var context = await DbContext();
        var author = context.Authors.AsEnumerable().First();
        var request = AddBookRequest(author.Id, "test", "test");
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var newBook = context.Books.AsEnumerable().OrderByDescending(x => x.Id).First();

        Assert.AreEqual(request.AuthorId, newBook.AuthorId);
        Assert.AreEqual(request.Title, newBook.Title);
        Assert.AreEqual(request.Description, newBook.Description);
    }

    [Test]
    public async Task AddBook_EmptyAuthor_Authenticated_BadRequest()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var request = AddBookRequest(0, "test", "test");
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task AddBook_NotExistedAuthor_Authenticated_BadRequest()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        var request = AddBookRequest(100, "test", "test");
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task AddBook_EmptyTitle_Authenticated_BadRequest()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        await using var context = await DbContext();
        var author = context.Authors.AsEnumerable().First();
        var request = AddBookRequest(author.Id, null, "test");
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task AddBook_LongTitle_Authenticated_BadRequest()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        await using var context = await DbContext();
        var author = context.Authors.AsEnumerable().First();
        var request = AddBookRequest(author.Id, RandomString(51), "test");
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task AddBook_LongDescription_Authenticated_BadRequest()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.AddBook;

        await using var context = await DbContext();
        var author = context.Authors.AsEnumerable().First();
        var request = AddBookRequest(author.Id, "test", RandomString(2001));
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task AddBook_Unauthorized()
    {
        var url = Urls.AddBook;

        await using var context = await DbContext();
        var author = context.Authors.AsEnumerable().First();
        var request = AddBookRequest(author.Id, "test", "test");
        var response = await apiClient.PostJson(url, request);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task AddBook_Forbidden()
    {
        var accessToken = await AuthenticateUser_EmptyScope();
        var url = Urls.AddBook;

        await using var context = await DbContext();
        var author = context.Authors.AsEnumerable().First();
        var request = AddBookRequest(author.Id, "test", "test");
        var response = await apiClient.PostJson(url, request, accessToken);
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }
}
