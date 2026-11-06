
//using Ecom.Domain.Entities.Products;

//namespace Ecom.Application.Interfaces
//{
//    public interface IProductRepository: IGenericRepository<Product>
//    {
//        Task<IEnumerable<Product>> GetAllAsync();
//        //Task<bool> AddAsync(AddProductDTO addProductDTO);
//        Task AddAsync(Product addProductDTO);
//        Task UpdateAsync(Product addProductDTO);
//        //Task<bool> UpdateAsync(UpdateProductDTO UpdateProductDTO);
//        Task DeleteAsync(Product product);
//        Task RemoveProductImagesAsync(int productId);

//    }
//}
using Ecom.Application.Features.Ratings.Dto;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Interfaces
{
    /// <summary>
    /// Rating-specific queries and commands that go beyond generic CRUD.
    /// The generic CRUD methods (GetByIdAsync, UpdateAsync, DeleteAsync …)
    /// are inherited from IGenericRepository&lt;Rating&gt;.
    /// </summary>
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        /// <summary>
        /// Adds a new rating for a product, preventing duplicate ratings per user.
        /// Also recalculates and persists the product's average rating.
        /// Returns false when the user already rated this product.
        /// </summary>
        Task<bool> AddRatingAsync(
            int stars,
            string content,
            int productId,
            string userEmail,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all ratings (with reviewer name) for a given product.
        /// </summary>
        Task<IReadOnlyList<ReturnRatingDTO>> GetAllRatingForProductAsync(
            int productId,
            CancellationToken cancellationToken = default);
        // Returns ALL ratings across every product — no auth required
        Task<IReadOnlyList<ReturnRatingDTO>> GetAllRatingsAsync(
            CancellationToken cancellationToken = default);
    }
}