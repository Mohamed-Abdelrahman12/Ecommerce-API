

using Ecom.Core.Entities;
using Ecom.Domain.Entities;
using Ecom.Domain.Entities.Order;
using Ecom.Domain.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecom.Infastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<ContactMessage> ContactMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
        

    }
  
}
 