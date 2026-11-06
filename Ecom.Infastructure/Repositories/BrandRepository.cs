

using Ecom.Application.Interfaces;
using Ecom.Domain.Entities.Products;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;

namespace Ecom.infrastructure.Repositories
{
    public class BrandRepository:GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext Context) : base(Context)
        {
        }
    }
}
