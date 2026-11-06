
using Ecom.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.OwnsOne(x => x.shippingAddress, x =>{ x.WithOwner(); });
            builder.HasMany(x => x.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.status).HasConversion(o => o.ToString(), o =>(Status) Enum.Parse(typeof(Status), o));
            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
