using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Common.Filters
{
    public class SortingFilter:IProductFilter
    {
        public IQueryable<Product> Apply(IQueryable<Product> query, ProductParams parameters)
        {
            if (string.IsNullOrEmpty(parameters.Sort))
                return query.OrderBy(x => x.Name);

            return parameters.Sort switch
            {
                "PriceAsn" => query.OrderBy(x => x.NewPrice),
                "PriceDec" => query.OrderByDescending(x => x.NewPrice),
                _ => query.OrderBy(x => x.Name),
            };
        }
    }
}
