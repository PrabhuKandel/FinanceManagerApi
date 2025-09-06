using System.Diagnostics;
using Serilog;

namespace FinanceManager.Api.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var traceId = context.TraceIdentifier;

            var request = context.Request;
            var routeValues = context.GetRouteData()?.Values;

            // Log request
            Log.Information(" Request | Method: {Method} | Path: {Path} | Controller: {Controller} | Action: {Action} | " +
                            "RouteParams: {@RouteParams} | QueryParams: {@QueryParams} | TraceId: {TraceId}",
                request.Method,
                request.Path,
                routeValues?["controller"] ?? "Unknown",
                routeValues?["action"] ?? "Unknown",
                routeValues?.Where(kvp => kvp.Key != "controller" && kvp.Key != "action")
                            .ToDictionary(k => k.Key, v => v.Value?.ToString() ?? "null"),
                request.Query.ToDictionary(q => q.Key, q => q.Value.ToString()),
                traceId
            );

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                

                // Log exception with full details
            

                throw;

                
            }

            stopwatch.Stop();

            // Log response
            var logLevel = context.Response.StatusCode >= 500 ? Serilog.Events.LogEventLevel.Error
                         : context.Response.StatusCode >= 400 ? Serilog.Events.LogEventLevel.Warning
                         : Serilog.Events.LogEventLevel.Information;

            Log.Write(logLevel,
                " Response | Method: {Method} | Path: {Path} | Controller: {Controller} | Action: {Action} | " +
                "StatusCode: {StatusCode} | DurationMs: {Duration} | TraceId: {TraceId} | UserId: {UserId} | Role: {UserRole}",
                request.Method,
                request.Path,
                routeValues?["controller"] ?? "Unknown",
                routeValues?["action"] ?? "Unknown",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                traceId,
                context.User?.FindFirst("userId")?.Value?? "Anonymous",
                context.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ??"No Role"



            );
        }
    }
}
