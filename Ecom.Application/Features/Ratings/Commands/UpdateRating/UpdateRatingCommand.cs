using MediatR;

namespace Ecom.Application.Features.Ratings.Commands.UpdateRating
{
    // IRequest<bool> — handler returns true on success, false if not found
    public class UpdateRatingCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
    }
}