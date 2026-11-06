using Ecom.Application.Features.WishList.Dto;
using Ecom.Application.Interfaces;
using Ecom.Core.Entities;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Repositories
{
    public class WishListRepository : GenericRepository<WishList>, IWishListRepository
    {
        private readonly AppDbContext _context;

        public WishListRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddToWishListAsync(
            int productId,
            string userEmail,
            CancellationToken cancellationToken = default)
        {
        
            var productExists = await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.Id == productId, cancellationToken);

            if (!productExists)
                return false;

   
            var alreadyExists = await _context.WishLists
                .AsNoTracking()
                .AnyAsync(w => w.UserEmail == userEmail && w.ProductId == productId, cancellationToken);

            if (alreadyExists)
                return false;

            var wishListItem = new WishList
            {
                UserEmail = userEmail,
                ProductId = productId,
            };

            await _context.WishLists.AddAsync(wishListItem, cancellationToken);
            await _context.SaveChangesAsync();
          
            return true;
        }

        public async Task<bool> DeleteWishListAsync(
            int wishListId,
            CancellationToken cancellationToken = default)
        {
            var item = await _context.WishLists
                .FirstOrDefaultAsync(w => w.Id == wishListId, cancellationToken);

            if (item is null)
                return false;

            _context.WishLists.Remove(item);
            await _context.SaveChangesAsync();
        
            return true;
        }
        public async Task<IReadOnlyList<WishListItemResponse>> GetAllWishListForUserAsync(
            string userEmail,
            CancellationToken cancellationToken = default)
        {
       
            var items = await _context.WishLists
                .Include(w => w.Product)
                    .ThenInclude(p => p.Photos)
                .AsNoTracking()
                .Where(w => w.UserEmail == userEmail)
                .ToListAsync(cancellationToken);

            return items.Select(w => new WishListItemResponse
            {
                WishListId = w.Id,
                ProductId = w.ProductId,
                Name = w.Product.Name,
                Description = w.Product.Description,
                NewPrice = w.Product.NewPrice,
                OldPrice = w.Product.OldPrice,
                Rating = w.Product.rating,
                Image = w.Product.Photos?.FirstOrDefault()?.ImageName ?? string.Empty,
            }).ToList();
        }
    }
}