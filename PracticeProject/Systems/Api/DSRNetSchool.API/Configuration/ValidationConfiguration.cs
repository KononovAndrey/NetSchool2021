namespace DSRNetSchool.API.Configuration;

using DSRNetSchool.Common.Helpers;
using DSRNetSchool.Common.Responses;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

public static class ValidatorConfiguration
{
    public static IMvcBuilder AddValidator(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var fieldErrors = new List<ErrorResponseFieldInfo>();
                foreach (var key in context.ModelState.Keys)
                {
                    fieldErrors.Add(new ErrorResponseFieldInfo()
                    {
                        FieldName = key,
                        Message = string.Join(", ", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });
                }

                var result = new BadRequestObjectResult(new ErrorResponse()
                {
                    Message = "One or more validation errors occurred.",
                    FieldErrors = fieldErrors
                });

                return result;
            };
        });

        builder.AddFluentValidation(fv =>
        {
            fv.DisableDataAnnotationsValidation = true;
            fv.ImplicitlyValidateChildProperties = true;
            fv.AutomaticValidationEnabled = true;
        });

        ValidatorsRegisterHelper.Register(builder.Services);

        //builder.Services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return builder;
    }
}