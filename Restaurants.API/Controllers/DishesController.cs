using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.AddDish;
using Restaurants.Application.Dishes.DTO;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

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
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishesForRestaurant(int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        
        return Ok(dishes);
    }
    
    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant(int restaurantId, int dishId)
    {
        var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
        
        return Ok(dish);
    }
}
