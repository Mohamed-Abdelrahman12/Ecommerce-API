using MediatR;

namespace Ecom.Application.Features.WishList.Commands.DeleteWishList
{
    public class DeleteWishListCommand : IRequest<bool>
    {
        public int WishListId { get; set; }

        public DeleteWishListCommand(int wishListId)
        {
            WishListId = wishListId;
        }
    }
}