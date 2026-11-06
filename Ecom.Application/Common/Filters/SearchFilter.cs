using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Common.Filters
{
    public class SearchFilter: IProductFilter
    {
    

        public IQueryable<Product> Apply(IQueryable<Product> query, ProductParams parameters)
        {
            if (string.IsNullOrEmpty(parameters.Search))
                return query;

            var searchWords = parameters.Search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return query.Where(m => searchWords.All(word =>
                m.Name.ToLower().Contains(word.ToLower()) ||
                m.Description.ToLower().Contains(word.ToLower())
            ));
        }
    }
}
