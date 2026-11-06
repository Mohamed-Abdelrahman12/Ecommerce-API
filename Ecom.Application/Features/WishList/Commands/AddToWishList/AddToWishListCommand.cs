using MediatR;

namespace Ecom.Application.Features.WishList.Commands.AddToWishList
{
    public class AddToWishListCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public string UserEmail { get; set; }
    }
}