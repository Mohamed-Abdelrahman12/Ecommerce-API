


using Ecom.Application.Interfaces;
using Ecom.Domain.Entities.Products;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;

namespace Ecom.infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext Context) : base(Context)
        {
        }
    }
}
