using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.AddRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Core.Entities;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllRestaurants()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());

        return Ok(restaurants);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

        return restaurant is null
            ? Problem("Restaurants not found", statusCode: 400, title: "Restaurant search")
            : Ok(restaurant);
    }

    [HttpPost]
    public async Task<ActionResult<Restaurant>> AddRestaurant(AddRestaurantCommand command)
    {
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Restaurant>> DeleteRestaurant(int id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));

        if (isDeleted) return NoContent();

        return Problem("Restaurant not found", statusCode: 400, title: "Restaurant deletion");
    }
}