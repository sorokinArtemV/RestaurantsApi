using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.API.Middlewares;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Exceptions;

namespace z_Restaurants.API.Tests.Middlewares;

public class ExceptionHandlingMiddlewareTest
{
    [Fact]
    public async Task InvokeAsync_ShouldCallNext_WhenNoExceptionThrown()
    {
        var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();
        
        //
        await middleware.InvokeAsync(context, nextDelegateMock.Object);
        
        //
        nextDelegateMock.Verify(x => x(context), Times.Once);
    }
    
    [Fact]
    public async Task InvokeAsync_ShouldSetStatusCode404_WhenNotFoundExceptionThrown()
    {
        var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var notFoundException = new NotFoundException(nameof(Restaurant), "1");
        
        //
        await middleware.InvokeAsync(context, _ => throw notFoundException);
        
        //
        context.Response.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task InvokeAsync_ShouldSetStatusCode403_WhenForbiddenExceptionThrown()
    {
        var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var forbidException = new ForbidException();
        
        //
        await middleware.InvokeAsync(context, _ => throw forbidException);
        
        //
        context.Response.StatusCode.Should().Be(403);
    }

    [Fact]
    public async Task InvokeAsync_ShouldSetStatusCode500_WhenOtherExceptionThrown()
    {
        var loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        var middleware = new ExceptionHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exception = new Exception();
        
        //
        await middleware.InvokeAsync(context, _ => throw exception);
        
        //
        context.Response.StatusCode.Should().Be(500);
   
    }
}