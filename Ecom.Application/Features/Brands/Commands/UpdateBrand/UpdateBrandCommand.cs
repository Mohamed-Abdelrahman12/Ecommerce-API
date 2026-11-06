using Ecom.Application.Features.Brands.Dto;
using MediatR;
namespace Ecom.Application.Features.Brands.Commands.UpdateBrand
{

    public record UpdateBrandCommand(
      UpdateBrandDTO UpdateBrandDTO
  ) : IRequest<bool>;
}
