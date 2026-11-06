using Ecom.Application.Features.Brands.Dto;
using MediatR;

namespace Ecom.Application.Features.Brands.Commands.CreateBrand
{
    public record CreateBrandCommand(BrandDTO BrandDTO) : IRequest<bool>;
}
