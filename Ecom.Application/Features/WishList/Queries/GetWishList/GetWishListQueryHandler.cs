using Ecom.Application.Features.WishList.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.WishList.Queries.GetWishList
{
    public class GetWishListQueryHandler : IRequestHandler<GetWishListQuery, IReadOnlyList<WishListItemResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWishListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<WishListItemResponse>> Handle(GetWishListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.wishListRepository
                .GetAllWishListForUserAsync(request.UserEmail, cancellationToken);
        }
    }
}