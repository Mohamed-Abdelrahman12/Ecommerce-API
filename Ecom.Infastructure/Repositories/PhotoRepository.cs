using Ecom.Application.Interfaces;
using Ecom.Domain.Entities.Products;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;

namespace Ecom.infrastructure.Repositories
{
    internal class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext Context) : base(Context)
        {
        }
    }
}
