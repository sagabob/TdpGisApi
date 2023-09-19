﻿using System.Net;
using Serilog;

namespace TdpGisApi.Endpoints.Middleware;

public class CustomExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlingMiddleware(RequestDelegate next
    )
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleGlobalExceptionAsync(context, ex);
        }
    }

    private Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is ApplicationException)
        {
            Log.ForContext("ValidationError", exception.Message)
                .Warning("Validation error occurred in API.");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsJsonAsync(new { exception.Message });
        }

        var errorId = Guid.NewGuid();
        Log.ForContext("ErrorId", errorId)
            .Error(exception, "Error occurred in API");
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsJsonAsync(new
        {
            ErrorId = errorId,
            Message = "Something bad happened in our API. " +
                      "Contact our support team with the ErrorId if the issue persists."
        });
    }
}