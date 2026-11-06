
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Domain.Entities.Products
{
    public class Photo:BaseEntity<int>
    {
        public string ImageName { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(name: nameof(ProductId))]
        public virtual Product Products { get; set; }

    }
}
