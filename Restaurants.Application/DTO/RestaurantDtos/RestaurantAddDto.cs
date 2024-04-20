using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.DTO.RestaurantDtos;

public class RestaurantAddDto
{
    [StringLength(maximumLength:100, MinimumLength = 3)]
    public string Name { get; set; } = default!;
    
    [StringLength(maximumLength: 500, MinimumLength = 3)]
    public string Description { get; set; } = default!;
    
    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? ContactEmail { get; set; }
    
    [Phone(ErrorMessage = "Invalid phone number")]
    public string? ContactNumber { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    
    [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Invalid postal code, valid example: 123456")]
    public string? PostalCode { get; set; }
}