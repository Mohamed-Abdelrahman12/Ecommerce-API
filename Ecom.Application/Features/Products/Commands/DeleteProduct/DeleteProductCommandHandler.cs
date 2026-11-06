using Ecom.Application.Interfaces;
using MediatR;


namespace Ecom.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _repo;

        public DeleteProductCommandHandler(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repo.ProductRepository.GetByIdAsync(

                request.ProductId,
                cancellationToken,
                x => x.Photos

            );

            if (product == null)
                return false;
            await _repo.ProductRepository.RemoveProductImagesAsync(product.Id);
            await _repo.ProductRepository.DeleteAsync(product);

            return true;
        }
    }
}
