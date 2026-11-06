

using Ecom.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasData(
     new Category { Id = 1, Name = "Laptops", Description = "Portable personal computers for work, study, and gaming." },
     new Category { Id = 2, Name = "Smartphones", Description = "Latest mobile phones with Android and iOS systems." },
     new Category { Id = 3, Name = "Watches", Description = "Smartwatches and classic wristwatches for everyday use." },
     new Category { Id = 4, Name = "Headphones", Description = "Wireless and wired headphones for music and calls." },
     new Category { Id = 5, Name = "Cameras", Description = "Digital cameras, DSLRs, and accessories." }
 );


        }
    }
}
