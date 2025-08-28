using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
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

            context.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new ExceptionResponse

            {

                Message = exception.Message,
                StatusCode = context.Response.StatusCode,
                
            };

            // Optionally include validation errors if it's a ValidationException
            //if (exception is ValidationException validationEx)
            //{
            //    return context.Response.WriteAsJsonAsync(new
            //    {
            //        error = exception.Message,
            //        statusCode = context.Response.StatusCode,
            //        validationErrors = validationEx.Errors
            //    });
            //}

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
