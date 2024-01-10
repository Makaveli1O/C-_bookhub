using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BusinessLayer.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly string _sourceName;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, string sourceName)
    {
        _next = next;
        _logger = logger;
        _sourceName = sourceName;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"{_sourceName} - Received request : {context.Request.Method} {context.Request.Path}");

        var watch = Stopwatch.StartNew();
        await _next(context);
        watch.Stop();

        _logger.LogInformation($"{_sourceName} - Response status code: {context.Response.StatusCode} and processing time: {watch.ElapsedMilliseconds} ms");
    }

}
