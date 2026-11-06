
namespace Ecom.Domain.Entities
{ 
    
    public class CustomerCart
    {
        public CustomerCart()
        {
            
        }
        public CustomerCart(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public  List<CartItems> cartItems { get; set; } = new List<CartItems>();

    }
}

