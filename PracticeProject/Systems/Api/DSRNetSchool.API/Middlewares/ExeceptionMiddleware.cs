﻿namespace DSRNetSchool.API.Middlewares;

using DSRNetSchool.Common.Extensions;
using DSRNetSchool.Common.Responses;
using System.Text.Json;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionsMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse response = null;
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            response = ex.ToErrorResponse();
        }
        finally
        {
            if (!(response is null))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
            }
        }
    }
}