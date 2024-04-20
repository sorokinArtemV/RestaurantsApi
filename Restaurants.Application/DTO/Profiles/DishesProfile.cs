using AutoMapper;
using Restaurants.Core.Entities;

namespace Restaurants.Application.DTO.Profiles;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>().ReverseMap();
    }
}