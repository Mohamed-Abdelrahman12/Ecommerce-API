using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Features.Products.Dto;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product,ProductDTO>()
           .ForMember(x => x.CategoryName, o=>o.MapFrom(src=>src.Category.Name))
         .ForMember(x => x.BrandName, o => o.MapFrom(src => src.Brand.Name))
         .ReverseMap();

            CreateMap<Photo, PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO, Product>()
                .ForMember(m=>m.Photos , op=>op.Ignore())
                .ReverseMap();

            CreateMap<UpdateProductDTO, Product>()
                .ForMember(m => m.Photos, op => op.Ignore())
                .ReverseMap();

        }

    }
}
