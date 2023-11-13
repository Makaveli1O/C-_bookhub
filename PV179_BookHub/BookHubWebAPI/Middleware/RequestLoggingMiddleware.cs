using System.Diagnostics;

namespace BookHubWebAPI.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"Received request: {context.Request.Method} {context.Request.Path}");

        var watch = Stopwatch.StartNew();
        await _next(context);
        watch.Stop();

        _logger.LogInformation($"Response status code: {context.Response.StatusCode} and processing time: {watch.ElapsedMilliseconds} ms");
    }
}
