using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.WishList.Commands.DeleteWishList
{
    public class DeleteWishListCommandHandler : IRequestHandler<DeleteWishListCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishListCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteWishListCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.wishListRepository.DeleteWishListAsync(
                wishListId: request.WishListId,
                cancellationToken: cancellationToken
            );

           
            return result;
        }
    }
}