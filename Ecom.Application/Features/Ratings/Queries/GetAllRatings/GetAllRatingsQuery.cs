using Ecom.Application.Features.Ratings.Dto;
using MediatR;

namespace Ecom.Application.Features.Ratings.Queries.GetAllRatings
{
    public class GetAllRatingsQuery : IRequest<IReadOnlyList<ReturnRatingDTO>>
    {
    }
}