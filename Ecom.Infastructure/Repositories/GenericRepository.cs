
using Ecom.Application.Interfaces;
using Ecom.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecom.Infastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _Context;

        public GenericRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _Context.Set<T>().AddAsync(entity,cancellationToken);
            await _Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        => await _Context.Set<T>().CountAsync(cancellationToken);

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _Context.Set<T>().FindAsync(id,cancellationToken);
            _Context.Set<T>().Remove(entity);

             await _Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _Context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
       

        public  async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            var Query = _Context.Set<T>().AsQueryable();

            foreach (var item in includes)
            {
                Query = Query.Include(item);
            }
            return await Query.ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _Context.Set<T>().FindAsync(id, cancellationToken);
            return entity;
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            var Query = _Context.Set<T>().AsQueryable();

            foreach (var item in includes)
            {
                Query = Query.Include(item);
            }
            var entity = await Query.FirstOrDefaultAsync(x=> EF.Property<int>(x, "Id") == id, cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _Context.Entry(entity).State = EntityState.Modified;
            await _Context.SaveChangesAsync(cancellationToken);
        }
    }
}
