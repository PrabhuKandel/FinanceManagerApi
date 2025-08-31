using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            IEnumerable<string>? errors = null;
            string message;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = exception.Message;

                    break;

                case CustomValidationException vex:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "Validation Failed";
                    errors = vex.Errors;
                    break;

                case DbUpdateException dbEx:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "A database error occurred";
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "An unexpected error occurred";
                    errors = new List<string> { exception.Message };
                    break;
            }

            context.Response.StatusCode = statusCode;

            var response = new ExceptionResponse
            {
                Message = message,
                StatusCode = statusCode,
                Errors = errors
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
