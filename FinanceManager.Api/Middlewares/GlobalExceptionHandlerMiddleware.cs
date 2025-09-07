using Azure.Core;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace FinanceManager.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next; // pointer to the next middleware
     

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
           
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

                // Log exception with full details

                var stopwatch = context.Items["ActionStopwatch"] as Stopwatch;
                var routeParams = context.Items["RouteParams"] as IDictionary<string, string>;
                var queryParams = context.Items["QueryParams"] as IDictionary<string, string>;
                var actionArgs = context.Items["ActionArgs"] as IDictionary<string, object>;

                var traceId = context.TraceIdentifier;

                // Stop stopwatch if running
                stopwatch?.Stop();

                // Log exception cleanly
                Log.Error(" Exception |Message: {Message} | Method: {Method} | Path: {Path} | Controller: {Controller} | Action: {Action} |" + "" +
                    " RouteParams: {@RouteParams} | QueryParams: {@QueryParams} | Action Args: {@ActionArgs} | " + 
                    "UserId: {UserId} | Role: {UserRole} | StatusCode: {StatusCode} | DurationMs: {Duration} | TraceId: {TraceId}", 
                    ex.Message,
                    context.Request.Method, context.Request.Path,
                    context.GetRouteData()?.Values?["controller"]??"Unknown",
                    context.GetRouteData()?.Values?["action"]??"Unknown",
                    routeParams, 
                    queryParams,
                    actionArgs,
                    context.User?.FindFirst("userId")?.Value ?? "Anonymous",
                    context.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? "No Role",
                    context.Response.StatusCode,
                    stopwatch?.ElapsedMilliseconds,
                    traceId );


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

                case ApiException apiEx:
                    statusCode = apiEx.StatusCode;
                    message = apiEx.Message;
                    errors = apiEx.Errors;
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
