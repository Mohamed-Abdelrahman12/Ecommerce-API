using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Common.Filters
{
    public class CategoryFilter : IProductFilter
    {
        public IQueryable<Product> Apply(IQueryable<Product> query, ProductParams parameters)
        {
            if (parameters.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == parameters.CategoryId.Value);

            return query;
        }
    }
}
