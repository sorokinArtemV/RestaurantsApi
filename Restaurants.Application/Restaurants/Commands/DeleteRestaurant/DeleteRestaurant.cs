using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurant(int id) : IRequest
{
    public int Id { get; } = id;
}