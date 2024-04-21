using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        
        var appAssembly = typeof(ServiceCollectionExtensions).Assembly;
        
        services.AddAutoMapper(appAssembly);

        services.AddValidatorsFromAssembly(appAssembly)
            .AddFluentValidationAutoValidation();
    }
}