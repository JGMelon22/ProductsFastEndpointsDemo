using System.ComponentModel.DataAnnotations;

namespace ProductsFastEndpointsDemo.Products.DTOs;

public record ProductRequest
(
    [Required(ErrorMessage = "Product name is required.")]
    [MinLength(2, ErrorMessage = "Product name must have at least 2 characters.")]
    [MaxLength(100, ErrorMessage = "Product name can not exceed 100 characters.")]
    string Name,

    [Required(ErrorMessage = "Product price is required.")]
    [Range(0.0, 9999.99, ErrorMessage = "Product price must be between 0.0 and 9999.99.")]
    decimal Price,

    [Required(ErrorMessage = "Product quantity is required.")]
    [MinLength(0, ErrorMessage = "Product quantity can not be less than 0.")]
    [MaxLength(10_000, ErrorMessage = "Product quantity can not be greater than 10_000.")]
    int Quantity,

    [Required(ErrorMessage = "Product availability must be informed.")]
    bool IsAvailable
);
