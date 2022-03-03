namespace DSRNetSchool.API.Controllers.Books.Models;

using AutoMapper;
using DSRNetSchool.Db.Entities;
using FluentValidation;

public class UpdateBookModel
{
    public string Title { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}

public class UpdateBookModelValidator : AbstractValidator<UpdateBookModel>
{
    public UpdateBookModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is long.");

        RuleFor(x => x.Note)
            .MaximumLength(2000).WithMessage("Description is long.");
    }
}

public class UpdateBookModelProfile : Profile
{
    public UpdateBookModelProfile()
    {
        CreateMap<UpdateBookModel, Book>();
    }
}