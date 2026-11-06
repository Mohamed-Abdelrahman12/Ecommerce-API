using Ecom.Application.Features.Ratings.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Ratings.Queries.GetRatings
{
    public class GetRatingsQueryHandler : IRequestHandler<GetRatingsQuery, IReadOnlyList<ReturnRatingDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRatingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ReturnRatingDTO>> Handle(GetRatingsQuery request, CancellationToken cancellationToken)
        {
            // Queries never write — they only read through the repository
            return await _unitOfWork.RatingRepository
                .GetAllRatingForProductAsync(request.ProductId, cancellationToken);
        }
    }
}