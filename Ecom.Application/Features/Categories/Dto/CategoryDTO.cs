using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Features.Categories.Dto
{
    public record CategoryDTO {

         public string  Name { get; set; }
        public string Description { get; set; }
           public IFormFile Image { get; set; }

    }
    public record CategoryResponseDTO(
        int Id,
        string Name,
        string Description,
        string? ImageUrl // This matches the string in your database
    );
    public record UpdateCategoryDTO: CategoryDTO
    {
       public int id { get; set; }


    }

     

     
}
