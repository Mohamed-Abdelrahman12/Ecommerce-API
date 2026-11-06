



namespace Ecom.Application.DTOs
{
    public record OrderDTO
    {
        public int deliveryMethodId { get; set; }
        public string cartId { get; set; }
        public ShippingAddressDTO ShippingAddress { get; set; }
    }
    public record ShippingAddressDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
    }
}
