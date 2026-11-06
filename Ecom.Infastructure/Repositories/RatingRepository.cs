
using Ecom.Application.Features.Ratings.Dto;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Ecom.Domain.Entities.Products;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositories
{
  
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RatingRepository(AppDbContext context, UserManager<AppUser> userManager)
            : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddRatingAsync(
            int stars,
            string content,
            int productId,
            string userEmail,
            CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
                return false;

            // Prevent duplicate rating from the same user for the same product
            bool alreadyRated = await _context.Ratings
                .AsNoTracking()
                .AnyAsync(r => r.AppUserId == user.Id && r.ProductId == productId, cancellationToken);

            if (alreadyRated)
                return false;

            var rating = new Rating
            {
                AppUserId = user.Id,
                ProductId = productId,
                Stars = stars,
                content = content,
            };

            await _context.Ratings.AddAsync(rating, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken); // first save: persist rating row

            // Recalculate the product's aggregate rating
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

            if (product is not null)
            {
                var allStars = await _context.Ratings
                    .AsNoTracking()
                    .Where(r => r.ProductId == productId)
                    .Select(r => r.Stars)
                    .ToListAsync(cancellationToken);

                // Round to nearest 0.5 (e.g. 3.7 → 3.5, 3.8 → 4.0)
                double average = allStars.Average();
                product.rating = Math.Round(average * 2, MidpointRounding.AwayFromZero) / 2;

                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }


        public async Task<IReadOnlyList<ReturnRatingDTO>> GetAllRatingForProductAsync(
            int productId,
            CancellationToken cancellationToken = default)
        {
            var ratings = await _context.Ratings
                .Include(r => r.AppUser)
                .AsNoTracking()
                .Where(r => r.ProductId == productId)
                .ToListAsync(cancellationToken);

            return ratings.Select(r => new ReturnRatingDTO
            {
                stars = r.Stars,
                content = r.content,
                userName = r.AppUser.UserName,
                ReviewTime = r.Review,
            }).ToList();
        }

        public async Task<IReadOnlyList<ReturnRatingDTO>> GetAllRatingsAsync(
    CancellationToken cancellationToken = default)
        {
            var ratings = await _context.Ratings
                .Include(r => r.AppUser)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return ratings.Select(r => new ReturnRatingDTO
            {
                stars = r.Stars,
                content = r.content,
                userName = r.AppUser.UserName,
                ReviewTime = r.Review,
            }).ToList();
        }
    }
}