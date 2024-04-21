using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.AddRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTO;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAllRestaurants()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());

        return Ok(restaurants);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurantById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

        return restaurant is null
            ? Problem("Restaurants not found", statusCode: 400, title: "Restaurant search")
            : Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant(AddRestaurantCommand command)
    {
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }

    [HttpPatch]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRestaurant(int id, UpdateRestaurantCommand command)
    {
        command.Id = id;

        var isUpdated = await mediator.Send(command);

        if (isUpdated) return NoContent();

        return Problem("Restaurant not found", statusCode: 400, title: "Restaurant update");
    }


    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));

        if (isDeleted) return NoContent();

        return Problem("Restaurant not found", statusCode: 400, title: "Restaurant deletion");
    }
}