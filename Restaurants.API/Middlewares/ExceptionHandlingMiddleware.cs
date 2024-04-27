using Restaurants.Core.Exceptions;

namespace Restaurants.API.Middlewares;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(ex.Message);

            logger.LogWarning(ex.Message);
        }

        catch (ForbidException ex)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access forbidden" + ex.Message);
        }

        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}