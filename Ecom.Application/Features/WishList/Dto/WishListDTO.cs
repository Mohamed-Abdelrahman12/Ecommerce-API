
namespace Ecom.Application.Features.WishList.Dto
{
  
    public record AddToWishListDTO
    {
        public int ProductId { get; init; }
    }

    
    public record WishListItemResponse
    {
        public int WishListId { get; init; }
        public int ProductId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Image { get; init; }
        public decimal NewPrice { get; init; }
        public decimal OldPrice { get; init; }
        public double Rating { get; init; }
    }
}