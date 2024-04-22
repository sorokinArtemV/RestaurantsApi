using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        const int SecondsTimeout = 4;

        var stopwatch = Stopwatch.StartNew();

        await next.Invoke(context);

        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds / 1000 > SecondsTimeout)
            logger.LogInformation("Request [{Verb}] at {Path} took {Elapsed} seconds",
                context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds / 1000);
    }
}