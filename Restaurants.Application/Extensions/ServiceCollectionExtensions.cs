using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants.ServiceContracts;
using Restaurants.Application.Restaurants.Services;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantsService, RestaurantsService>();
        
        var appAssembly = typeof(ServiceCollectionExtensions).Assembly;
        
        services.AddAutoMapper(appAssembly);

        services.AddValidatorsFromAssembly(appAssembly)
            .AddFluentValidationAutoValidation();
    }
}