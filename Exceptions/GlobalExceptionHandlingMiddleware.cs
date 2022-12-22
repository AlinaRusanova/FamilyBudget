using FamilyFinance.Exceptions.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using NotImplementedException = FamilyFinance.Exceptions.Exceptions.NotImplementedException;
using UnauthorizedAccessException = FamilyFinance.Exceptions.Exceptions.UnauthorizedAccessException;
using KeyNotFoundException = FamilyFinance.Exceptions.Exceptions.KeyNotFoundException;

namespace FamilyFinance.Exceptions
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message;

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(IncorrectPasswordException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(IncorrectUserException))
            {
                status = HttpStatusCode.NotFound;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotRegisteredException))
            {
                status = HttpStatusCode.Forbidden;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotUniqueUsernameException))
            {
                status = HttpStatusCode.Forbidden;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }

            var exceptionResult = "";
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            }
            else { exceptionResult = JsonSerializer.Serialize(new { error = message }); }
          
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            await context.Response.WriteAsync(exceptionResult);
        }
    }
}