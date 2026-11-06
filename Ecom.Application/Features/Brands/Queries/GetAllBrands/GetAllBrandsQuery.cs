using Ecom.Application.Features.Brands.Dto;
using MediatR;
namespace Ecom.Application.Features.Brands.Queries.GetAllBrands
{
    public record GetAllBrandsQuery : IRequest<IReadOnlyList<BrandResponseDTO>>;

}
