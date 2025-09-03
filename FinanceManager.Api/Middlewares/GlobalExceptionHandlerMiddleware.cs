using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next; // pointer to the next middleware
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //Calls that method for each HTTP request that reaches this middleware in the pipeline.
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {   //calling the next middlware in pipeline (could be routing, authentication, or the controller action ).
                //if everything works normally, the request continues down the pipeline
                // If any of middleware throw an exception, it immediately stops execution in that downstream place and bubbles back.
                //he  try-catch  catches that exception  which is  in our global exception middleware.
            //   Each middleware constructor receives a RequestDelegate next.
            //That next is a reference to the next middleware’s function.
                //When you call _next(context),
                //you’re just invoking that delegate → which executes the next middleware with the same HttpContext.
                await _next(context); 
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                Log.Error("Exception occurred for request {Method} {Path} | ErrorMessage: {ErrorMessage}",
                 context.Request.Method,
                 context.Request.Path,
                 ex.Message);


                await HandleExceptionAsync(context, ex);
            }   
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            IDictionary<string, string>? errors=null ;
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
                    errors = new Dictionary<string, string> { { "Error", exception.Message } };
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
