using Ecom.Application.Features.Brands.Dto;
using MediatR;

namespace Ecom.Application.Features.Brands.Queries.GetBrandDetails
{

    public record GetBrandByIdQuery(int Id) : IRequest<BrandDTO>;
}
