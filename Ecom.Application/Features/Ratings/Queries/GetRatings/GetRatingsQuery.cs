using Ecom.Application.Features.Ratings.Dto;
using MediatR;

namespace Ecom.Application.Features.Ratings.Queries.GetRatings
{
    public class GetRatingsQuery : IRequest<IReadOnlyList<ReturnRatingDTO>>
    {
        public int ProductId { get; set; }

        public GetRatingsQuery(int productId)
        {
            ProductId = productId;
        }
    }
}