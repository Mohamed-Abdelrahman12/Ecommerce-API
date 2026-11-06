
//using Ecom.Application.DTOs;
//using Ecom.Core.Entities;

//namespace Ecom.Application.Interfaces
//{
//    public interface IWishListRepository
//    {
//        Task<WishList> CreateWishListAsync(WishListDTO wishListDTO, string UserEmail);
//        Task<IReadOnlyList<WishListResponse>> GetAllWisListForUserAsync(string UserEmail);
//        Task<bool> DeleteWishListAsync(int id);
//    }
//}
using Ecom.Application.Features.WishList.Dto;
using Ecom.Core.Entities;

namespace Ecom.Application.Interfaces
{
    public interface IWishListRepository : IGenericRepository<WishList>
    {
        /// <summary>
        /// Adds a product to the user's wishlist.
        /// Returns false if the product does not exist or is already wishlisted.
        /// </summary>
        Task<bool> AddToWishListAsync(
            int productId,
            string userEmail,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all wishlist items for the given user,
        /// with full product details projected into the response DTO.
        /// </summary>
        Task<IReadOnlyList<WishListItemResponse>> GetAllWishListForUserAsync(
            string userEmail,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a wishlist item by its own ID.
        /// Returns false if the item was not found.
        /// </summary>
        Task<bool> DeleteWishListAsync(
            int wishListId,
            CancellationToken cancellationToken = default);
    }
}