using Ecom.Application.Features.WishList.Dto;
using MediatR;

namespace Ecom.Application.Features.WishList.Queries.GetWishList
{
    public class GetWishListQuery : IRequest<IReadOnlyList<WishListItemResponse>>
    {
        public string UserEmail { get; set; }

        public GetWishListQuery(string userEmail)
        {
            UserEmail = userEmail;
        }
    }
}