using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.DTO;
using Restaurants.Application.Restaurants.ServiceContracts;
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
            ? Problem("Restaurants not found", statusCode: 400, title: "Restaurant search")
            : Ok(restaurant);
    }

    [HttpPost]
    public async Task<ActionResult<Restaurant>> AddRestaurant(RestaurantAddDto restaurantAddDto)
    {
        var id = await restaurantsService.AddRestaurant(restaurantAddDto);

        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }
}