using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Common.Filters
{
    public class RatingFilter : IProductFilter
    {
        public IQueryable<Product> Apply(IQueryable<Product> query, ProductParams parameters)
        {
            if (parameters.Rating.HasValue)
                query = query.Where(x => x.rating == parameters.Rating.Value);

            return query;
        }
    }
}
