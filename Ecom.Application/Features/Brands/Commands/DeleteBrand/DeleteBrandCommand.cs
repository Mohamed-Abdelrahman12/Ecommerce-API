using MediatR;

namespace Ecom.Application.Features.Brands.Commands.DeleteBrand
{

    public record DeleteBrandCommand(int id) : IRequest<bool>;
}
