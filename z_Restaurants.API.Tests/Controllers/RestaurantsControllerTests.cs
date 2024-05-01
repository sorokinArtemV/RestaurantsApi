using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace z_Restaurants.API.Tests.Controllers;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAll_ShouldReturn200OK_WhenRequestIsSuccessful()
    {
        var client = _factory.CreateClient();

        var result = await client.GetAsync("/api/restaurants?pageSize=10&pageNumber=1");
        
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetAll_ShouldReturn400BadRequest_WhenRequestIsInvalid()
    {
        var client = _factory.CreateClient();

        var result = await client.GetAsync("/api/restaurants");
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}