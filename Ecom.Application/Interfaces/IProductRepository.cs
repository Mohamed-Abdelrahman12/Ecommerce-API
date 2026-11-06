
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync();
        //Task<bool> AddAsync(AddProductDTO addProductDTO);
        Task AddAsync(Product addProductDTO);
        Task UpdateAsync(Product addProductDTO);
        //Task<bool> UpdateAsync(UpdateProductDTO UpdateProductDTO);
        Task DeleteAsync(Product product);
        Task RemoveProductImagesAsync(int productId);

    }
}
