using AutoMapper;
using Restaurants.Core.Entities;


namespace Restaurants.Application.DTO.Restaurant;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<AddRestaurantDto, Core.Entities.Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(s => new Address
            {
                City = s.City,
                PostalCode = s.PostalCode,
                Street = s.Street
            }));
        
        CreateMap<Core.Entities.Restaurant, RestaurantDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.City))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.PostalCode))
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.Street))
            .ForMember(d => d.Dishes, opts => opts.MapFrom(s => s.Dishes))
            .ReverseMap();
    }
}