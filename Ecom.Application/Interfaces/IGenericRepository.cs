

using System.Linq.Expressions;

namespace Ecom.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default); 
    }
}
/*
 EcommerceSolution
│
└── src
    ├── Ecom.API
    │   ├── Controllers
    │   │   ├── ProductsController.cs
    │   │   ├── BrandsController.cs
    │   │   └── ...
    │   ├── Middleware
    │   ├── Mapping
    │   │   └── MappingProfile.cs
    │   ├── appsettings.json
    │   └── Program.cs
    │
    ├── Ecom.Application
    │   ├── Common
    │   │   ├── Behaviors
    │   │   │   └── ValidationBehavior.cs
    │   │   └── Interfaces
    │   │       └── IProductRepository.cs
    │   │
    │   ├── Products
    │   │   ├── Commands
    │   │   │   ├── CreateProduct
    │   │   │   │   ├── CreateProductCommand.cs
    │   │   │   │   ├── CreateProductCommandHandler.cs
    │   │   │   │   └── CreateProductCommandValidator.cs
    │   │   │   ├── UpdateProduct
    │   │   │   └── DeleteProduct
    │   │   ├── Queries
    │   │   │   ├── GetAllProducts
    │   │   │   │   ├── GetAllProductsQuery.cs
    │   │   │   │   └── GetAllProductsQueryHandler.cs
    │   │   │   └── GetProductById
    │   │   └── Dtos
    │   │       └── ProductDto.cs
    │   │
    │   ├── Brands
    │   │   ├── Commands
    │   │   └── Queries
    │   └── DependencyInjection.cs
    │
    ├── Ecom.Domain
    │   ├── Entities
    │   │   ├── Product.cs
    │   │   ├── Brand.cs
    │   │   └── Category.cs
    │   ├── ValueObjects
    │   ├── Events
    │   └── Common
    │       └── BaseEntity.cs
    │
    └── Ecom.Infrastructure
        ├── Data
        │   └── EcomDbContext.cs
        ├── Repositories
        │   ├── ProductRepository.cs
        │   └── BrandRepository.cs
        ├── Migrations
        ├── DependencyInjection.cs
        └── Services
 
 
 
 */