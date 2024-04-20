using AutoMapper;
using Restaurants.Core.Entities;

namespace Restaurants.Application.DTO.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.City))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.PostalCode))
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.Street))
            .ForMember(d => d.Dishes, opts => opts.MapFrom(s => s.Dishes))
            .ReverseMap();
    }
}