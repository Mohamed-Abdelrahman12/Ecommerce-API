
using Ecom.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.infrastructure.Data.Config
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
           
            builder.Property(x => x.UserEmail)
                .IsRequired()
                .HasMaxLength(256);
          
            builder.HasIndex(x => new { x.UserEmail, x.ProductId })
                .IsUnique();

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}