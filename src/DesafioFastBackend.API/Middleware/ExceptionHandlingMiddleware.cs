using System.Net;
using System.Text.Json;
using DesafioFastBackend.API.Contracts;
using DesafioFastBackend.Domain.Exceptions;
using FluentValidation;

namespace DesafioFastBackend.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, errors) = exception switch
        {
            ValidationException validationException =>
                ((int)HttpStatusCode.BadRequest,
                "Falha de validação.",
                validationException.Errors.Select(x => x.ErrorMessage).Distinct()),

            ConflictException conflictException =>
                ((int)HttpStatusCode.Conflict,
                conflictException.Message,
                Enumerable.Empty<string>()),

            BusinessRuleException businessRuleException =>
                ((int)HttpStatusCode.BadRequest,
                businessRuleException.Message,
                Enumerable.Empty<string>()),

            _ =>
                ((int)HttpStatusCode.InternalServerError,
                "Erro interno inesperado.",
                Enumerable.Empty<string>())
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ApiErrorResponse
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode,
            TraceId = context.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
