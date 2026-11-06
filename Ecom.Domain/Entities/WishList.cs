
using Ecom.Domain.Entities;
using Ecom.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.Entities
{
    public class WishList : BaseEntity<int>
    {
        public string UserEmail { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}