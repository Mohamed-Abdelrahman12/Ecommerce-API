namespace Ecom.Application.Features.Ratings.Dto
{
    public class RatingDTO
    {
        public int stars { get; set; }
        public string content { get; set; }

        public int productId { get; set; }
    }
    public class UpdateRatingDTO
    {
        public int id { get; set; }
        public int stars { get; set; }
        public string content { get; set; }

        public int productId { get; set; }
    }
    public class ReturnRatingDTO
    {
        public int stars { get; set; }
        public string content { get; set; }

        public string userName { get; set; }
        public DateTime ReviewTime { get; set; }
    }
}
