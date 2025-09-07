using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace FinanceManager.Api.Filters
{
    public class RequestResponseLoggingFillter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            var stopwatch = Stopwatch.StartNew();
      
            var traceId = httpContext.TraceIdentifier;

            var request = httpContext.Request;

            var routeValues = httpContext.GetRouteData()?.Values;

            // Query parameters
            var queryParams = request.Query.ToDictionary(q => q.Key, q => q.Value.ToString());

            // Route parameters
            var routeParams = routeValues?
                              .Where(kvp => kvp.Key != "controller" && kvp.Key != "action")
                              .ToDictionary(k => k.Key, v => v.Value?.ToString() ?? "null");

            var actionArgs = context.ActionArguments;


            // Store in HttpContext for later use
            httpContext.Items["ActionStopwatch"] = stopwatch; //storing stopwatch instances in httpcontext.items
            httpContext.Items["RouteParams"] = routeParams;
            httpContext.Items["QueryParams"] = queryParams;
            httpContext.Items["ActionArgs"] = actionArgs;

            // Log request
            Log.Information(" Request | Method: {Method} | Path: {Path} | Controller: {Controller} | Action: {Action} | " + "" +
                " | RouteParams: {@RouteParams} | QueryParams: {@QueryParams} | Action Args: {@ActionArgs} | UserId: {UserId} | Role: {UserRole} | TraceId: {TraceId}"
                ,request.Method,
                request.Path,
                routeValues?["controller"] ?? "Unknown",
                routeValues?["action"] ?? "Unknown",
                queryParams, 
                routeParams,
                actionArgs,
                httpContext.User?.FindFirst("userId")?.Value ?? "Anonymous", 
                httpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? "No Role", 
                traceId);


            //before the action executes.
            var executedContext = await next();

            //after the aciton executes

            //Log response
                var logLevel = httpContext.Response.StatusCode >= 500 ? Serilog.Events.LogEventLevel.Error 
                : httpContext.Response.StatusCode >= 400 ? Serilog.Events.LogEventLevel.Warning 
                : Serilog.Events.LogEventLevel.Information; 

            if (executedContext.Exception == null)
            { 
                stopwatch.Stop();
                Log.Write(logLevel, " Response | Method: {Method} | Path: {Path} | Controller: {Controller} | Action: {Action} | " +
                    "" + " RouteParams: {@RouteParams} | QueryParams: {@QueryParams} | Action Args: {@ActionArgs} | " 
                    + "UserId: {UserId} | Role: {UserRole} | StatusCode: {StatusCode} | DurationMs: {Duration} | TraceId: {TraceId}",
                    request.Method,
                    request.Path,
                    routeValues?["controller"] ?? "Unknown",
                    routeValues?["action"] ?? "Unknown", 
                    queryParams, 
                    routeParams,
                    actionArgs,
                    httpContext.User?.FindFirst("userId")?.Value ?? "Anonymous",
                    httpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? "No Role", 
                    httpContext.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds,
                    traceId ); 
            }

          

        }
    }
}
