

namespace Ecom.Application.Features.Products.Commands.DeleteProduct
{
    using MediatR;

    public record DeleteProductCommand(int ProductId) : IRequest<bool>;

}
