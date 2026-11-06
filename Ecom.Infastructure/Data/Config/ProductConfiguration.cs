
using Ecom.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.Property(x=> x.Name).IsRequired();
           builder.Property(x=> x.NewPrice).HasColumnType("decimal(18,2)").IsRequired();
           builder.Property(x=> x.OldPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x=> x.Description).IsRequired();
        }
    }
}
