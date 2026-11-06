using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Common.Filters
{
    public class BrandFilter : IProductFilter
    {
        public IQueryable<Product> Apply(IQueryable<Product> query, ProductParams parameters)
        {
            if (parameters.BrandId.HasValue)
                query = query.Where(x => x.BrandId == parameters.BrandId.Value);

            return query;
        }
    }
}
