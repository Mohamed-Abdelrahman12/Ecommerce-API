

using Ecom.Application.Sharing;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Interfaces
{
    public interface IProductFilter
    {
        IQueryable<Product> Apply(IQueryable<Product> query, ProductParams parameters);
    }

}
