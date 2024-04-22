using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.AddDish;

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
}