using Ecom.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.infrastructure.Data.Config
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasData(new DeliveryMethod[]
       {
    new DeliveryMethod
    {
        Id = 1,
        DeleveryTime = "1-3 business days",
        Description = "Fast and reliable international delivery by DHL.",
        Name = "DHL Express",
        Price = 20
    },
    new DeliveryMethod
    {
        Id = 2,
        DeleveryTime = "3-5 business days",
        Description = "Affordable shipping with FedEx Ground services.",
        Name = "FedEx",
        Price = 15
    },
    new DeliveryMethod
    {
        Id = 3,
        DeleveryTime = "5-7 business days",
        Description = "Standard national and international postal delivery.",
        Name = "UPS",
        Price = 10
    },
    new DeliveryMethod
    {
        Id = 4,
        DeleveryTime = "7-14 business days",
        Description = "Economy shipping option for non-urgent deliveries.",
        Name = "Standard Post",
        Price = 5
    }
       });

        }
    }
}
