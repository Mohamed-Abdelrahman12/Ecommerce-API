using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities.Products;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Ecom.infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepository(AppDbContext Context , IImageManagementService imageManagementService) : base(Context)
        {
            context = Context;
            _imageManagementService = imageManagementService;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Photos)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task AddAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

        }
        public async Task UpdateAsync(Product product )
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Product product)
{
    context.Products.Remove(product);
    await context.SaveChangesAsync();
}
        public async Task RemoveProductImagesAsync(int productId)
        {
            var photos = await context.Photos
                .Where(p => p.ProductId == productId)
                .ToListAsync();

            foreach (var img in photos)
                _imageManagementService.DeleteImageAsync(img.ImageName);

            context.Photos.RemoveRange(photos);
            await context.SaveChangesAsync();
        }

        
    }
}
