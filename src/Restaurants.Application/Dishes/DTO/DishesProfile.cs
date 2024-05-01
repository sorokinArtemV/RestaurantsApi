using AutoMapper;
using Restaurants.Application.Dishes.Commands.AddDish;
using Restaurants.Core.Domain.Entities;

namespace Restaurants.Application.Dishes.DTO;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<AddDishCommand, Dish>().ReverseMap();
        CreateMap<Dish, DishDto>().ReverseMap();
    }
}