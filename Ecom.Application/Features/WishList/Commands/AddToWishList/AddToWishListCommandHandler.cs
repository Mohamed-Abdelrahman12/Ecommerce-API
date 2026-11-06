using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.WishList.Commands.AddToWishList
{
    public class AddToWishListCommandHandler : IRequestHandler<AddToWishListCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddToWishListCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddToWishListCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.wishListRepository.AddToWishListAsync(
                productId: request.ProductId,
                userEmail: request.UserEmail,
                cancellationToken: cancellationToken
            );

          

            return result;
        }
    }
}