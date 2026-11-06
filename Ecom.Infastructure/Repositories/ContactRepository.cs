using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;

namespace Ecom.infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<ContactMessage>, IContactRepository
    {
        public ContactRepository(AppDbContext Context) : base(Context)
        {
        }
    }
}
