namespace DSRNetSchool.Api.Test.Tests.Component.Book;

using DSRNetSchool.Api.Test.Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestFixture]
public partial class BookTest : ComponentTest
{
    protected static class Urls
    {
        public static string GetBooks(int? offset = null, int? limit = null)
        {

            if (offset is null && limit is null)
                return $"/api/v1/books";
            List<string> queryParameters = new List<string>();

            if (offset.HasValue)
            {
                queryParameters.Add($"offset={offset}");
            }

            if (limit.HasValue)
            {
                queryParameters.Add($"limit={limit}");
            }

            var queryString = string.Join("&", queryParameters);
            return $"/api/v1/books?{queryString}";
        }

        public static string GetBook(string id) => $"/api/v1/books/{id}";

        public static string DeleteBook(string id) => $"/api/v1/books/{id}";

        public static string UpdateBook(string id) => $"/api/v1/books/{id}";

        public static string AddBook => $"/api/v1/books";
    }

    public static class Scopes
    {
        public static string ReadBooks => "offline_access books_read";

        public static string WriteBooks => "offline_access books_write";

        public static string ReadAndWriteBooks => "offline_access books_read books_write";

        public static string Empty => "offline_access";
    }

    public async Task<string> AuthenticateUser_ReadAndWriteBooksScope()
    {
        var user = GetTestUser();
        var tokenResponse = await AuthenticateTestUser(user.Username, user.Password, Scopes.ReadAndWriteBooks);
        return tokenResponse.AccessToken;
    }

    public async Task<string> AuthenticateUser_EmptyScope()
    {
        var user = GetTestUser();
        var tokenResponse = await AuthenticateTestUser(user.Username, user.Password, Scopes.Empty);
        return tokenResponse.AccessToken;
    }
}
