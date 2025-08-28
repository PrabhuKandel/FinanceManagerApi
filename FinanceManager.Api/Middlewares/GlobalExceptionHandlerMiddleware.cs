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
            string message = exception.Message;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case CustomValidationException vex:
                    statusCode = StatusCodes.Status400BadRequest;
                    errors = vex.Errors;
                    break;


                default:
                    statusCode = StatusCodes.Status500InternalServerError;
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
