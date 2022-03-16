namespace DSRNetSchool.Api.Test.Tests.Component.Book;

public partial class BookIntegrationTest
{
    public static class Generator
    {
        public static string[] ValidTitles =
        {
            new string('1',1),
            new string('1',20),
            new string('1',50)
        };

        public static string[] InvalidTitles =
        {
            null,
            "",
            new string('1',51),
        };

        public static string[] ValidDescriptions =
        {
            null,
            "",
            new string('1',1),
            new string('1',2000),
        };

        public static string[] InvalidDescriptions =
        {
            new string('1',2001),
        };

        public static int[] InvalidAuthorIds =
        {
            0,
            -1,
            int.MaxValue
        };
    }
}
