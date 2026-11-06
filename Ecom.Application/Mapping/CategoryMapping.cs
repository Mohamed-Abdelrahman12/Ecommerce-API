using AutoMapper;
using Ecom.Application.Features.Categories.Dto;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {

            CreateMap<Category, CategoryResponseDTO>();

            CreateMap<CategoryDTO, Category>().ForMember(x => x.ImageUrl, opt => opt.Ignore());
            CreateMap<UpdateCategoryDTO, Category>().ForMember(x => x.ImageUrl, opt => opt.Ignore());
        }
    }
}
    
