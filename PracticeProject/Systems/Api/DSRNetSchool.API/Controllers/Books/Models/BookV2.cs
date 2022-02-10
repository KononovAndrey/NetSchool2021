﻿namespace DSRNetSchool.API.Controllers.Books.Models;

public class BookV2
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
