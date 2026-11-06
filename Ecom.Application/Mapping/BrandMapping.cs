using AutoMapper;
using Ecom.Application.Features.Brands.Dto;
using Ecom.Domain.Entities.Products;


namespace Ecom.Application.Mapping
{
    public class BrandMapping : Profile
    {
        public BrandMapping()
        {
            CreateMap<Brand, BrandResponseDTO>();

            CreateMap<BrandDTO, Brand>().ForMember(x => x.ImageUrl, opt => opt.Ignore());
            CreateMap<UpdateBrandDTO, Brand>().ForMember(x => x.ImageUrl, opt => opt.Ignore());
        }
    }
}
