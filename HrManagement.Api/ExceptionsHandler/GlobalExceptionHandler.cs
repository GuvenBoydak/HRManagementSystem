using System.Net;
using FluentValidation;
using HrManagement.Application;
using Microsoft.AspNetCore.Diagnostics;

namespace HrManagement.Api.ExceptionsHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var errorDto = ServiceResult.Failure(exception.Message, statusCode);

        if (exception is ValidationException validationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            errorDto = ServiceResult.Failure(validationException.Errors.Select(x => x.PropertyName).ToList(),
                statusCode);
        }

        httpContext.Response.StatusCode = (int)statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(errorDto, cancellationToken);

        return await ValueTask.FromResult(true);
    }
}