using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Features.Brands.Dto
{
    public record BrandDTO
     (string Name,
      string Description,
          IFormFile ImageUrl 
      );
    public record UpdateBrandDTO
           (string Name,
     string Description,
     int Id,
      IFormFile? ImageUrl
     );
    public record BrandResponseDTO(
     int Id,
     string Name,
     string Description,
     string? ImageUrl 
 );
}
