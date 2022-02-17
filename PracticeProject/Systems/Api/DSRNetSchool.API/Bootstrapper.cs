﻿namespace DSRNetSchool.API;

using DSRNetSchool.BookService;
using DSRNetSchool.Settings;

public static class Bootstrapper
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services
            .AddSettings()
            .AddBookService();
    }
}