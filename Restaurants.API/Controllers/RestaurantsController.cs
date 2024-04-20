using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTO.RestaurantDtos;
using Restaurants.Application.ServiceContracts;
using Restaurants.Core.Entities;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllRestaurants()
    {
        var restaurants = await restaurantsService.GetAllRestaurants();

        return Ok(restaurants);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
    {
        var restaurant = await restaurantsService.GetRestaurantById(id);

        return restaurant is null
            ? Problem("RestaurantDtos not found", statusCode: 400, title: "RestaurantDtos search")
            : Ok(restaurant);
    }

    [HttpPost]
    public async Task<ActionResult<Restaurant>> AddRestaurant(RestaurantAddDto restaurantAddDto)
    {
        var id = await restaurantsService.AddRestaurant(restaurantAddDto);

        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }
}