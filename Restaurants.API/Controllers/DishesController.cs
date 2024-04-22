using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.AddDish;
using Restaurants.Application.Dishes.DTO;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddDish(int restaurantId, AddDishCommand command)
    {
        command.RestaurantId = restaurantId;

        await mediator.Send(command);

        return Created();
    }

    [HttpGet]
    public async Task<IEnumerable<DishDto>> GetAllDishesForRestaurant(int restaurantId)
    {
        var dishes = await mediator.GetDishesForRestaurantQuery(restaurantId);

        return Ok(dishes);
    }
}