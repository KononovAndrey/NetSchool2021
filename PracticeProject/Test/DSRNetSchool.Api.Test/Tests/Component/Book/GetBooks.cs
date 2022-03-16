namespace DSRNetSchool.Api.Test.Tests.Component.Book;

using DSRNetSchool.Api.Test.Common.Extensions;
using DSRNetSchool.API.Controllers.Books.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

/// <summary>
/// Get books tests
/// </summary>
public partial class BookIntegrationTest
{
    [Test]
    public async Task GetBooks_ValidParameters_Authenticated_OkResponse()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.GetBooks();
        var response = await apiClient.Get(url, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var books_from_api = await response.ReadAsObject<IEnumerable<BookResponse>>();

        await using var context = await DbContext();
        var books_from_db = context.Books.AsEnumerable();

        Assert.AreEqual(books_from_db.Count(), books_from_api.Count());
    }

    [Test]
    public async Task GetBooks_NegativeParameters_OkResponse()
    {
        var accessToken = await AuthenticateUser_ReadAndWriteBooksScope();
        var url = Urls.GetBooks(-1, -1);
        var response = await apiClient.Get(url, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var books_from_api = await response.ReadAsObject<IEnumerable<BookResponse>>();

        Assert.AreEqual(0, books_from_api.Count());
    }

    [Test]
    public async Task GetBooks_Unauthorized()
    {
        var url = Urls.GetBooks();
        var response = await apiClient.Get(url);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task GetBooks_EmptyScope_Forbidden()
    {
        var accessToken = await AuthenticateUser_EmptyScope();
        var url = Urls.GetBooks();
        var response = await apiClient.Get(url, accessToken);
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }
}
