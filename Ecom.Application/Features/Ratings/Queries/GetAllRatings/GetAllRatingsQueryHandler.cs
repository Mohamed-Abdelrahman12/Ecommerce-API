using Ecom.Application.Features.Ratings.Dto;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Ratings.Queries.GetAllRatings
{
    public class GetAllRatingsQueryHandler : IRequestHandler<GetAllRatingsQuery, IReadOnlyList<ReturnRatingDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRatingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ReturnRatingDTO>> Handle(GetAllRatingsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RatingRepository.GetAllRatingsAsync(cancellationToken);
        }
    }
}