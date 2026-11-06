namespace Ecom.Domain.Entities.Order
{
    public class DeliveryMethod:BaseEntity<int>
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string name, decimal price, string deleveryTime, string description)
        {
            Name = name;
            Price = price;
            DeleveryTime = deleveryTime;
            Description = description;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DeleveryTime { get; set; }
        public string Description { get; set; }

    }
}