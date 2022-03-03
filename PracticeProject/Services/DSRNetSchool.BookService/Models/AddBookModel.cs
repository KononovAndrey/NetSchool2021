﻿namespace DSRNetSchool.API.Controllers.Books.Models;

using AutoMapper;
using DSRNetSchool.Db.Entities;
using FluentValidation;

public class AddBookModel
{
    public string Title { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}

public class AddBookModelValidator : AbstractValidator<AddBookModel>
{
    public AddBookModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is long.");

        RuleFor(x => x.Note)
            .MaximumLength(2000).WithMessage("Description is long.");
    }
}

public class AddBookModelProfile : Profile
{
    public AddBookModelProfile()
    {
        CreateMap<AddBookModel, Book>();
    }
}