using Ecom.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.infrastructure.Data.Config
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasData(
    new Brand { Id = 1, Name = "Apple", Description = "Innovative technology company known for iPhone, MacBook, and more." },
    new Brand { Id = 2, Name = "Samsung", Description = "Leading global manufacturer of electronics, smartphones, and appliances." },
    new Brand { Id = 3, Name = "Sony", Description = "Japanese brand famous for high-quality electronics, gaming, and entertainment products." },
    new Brand { Id = 4, Name = "Dell", Description = "Trusted PC and technology company producing laptops and desktops for all users." },
    new Brand { Id = 5, Name = "Canon", Description = "Renowned for cameras, imaging, and optical products." }
);

        }
    }
}
