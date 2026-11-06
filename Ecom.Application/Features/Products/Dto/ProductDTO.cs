using Ecom.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Features.Products.Dto
{
    public record ProductDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public virtual List<PhotoDTO> Photos { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public double rating { get; set; }

    }
    public record ReturnProductDTO
    {
        public List<ProductDTO> products { get; set; }
        public int TotalCount { get; set; }
    }
    public record AddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public IFormFileCollection Photo { get; set; }
    }
    public record UpdateProductDTO: AddProductDTO
    {
        public int Id { get; set; }
    }
}
