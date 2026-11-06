using MediatR;

namespace Ecom.Application.Features.Ratings.Commands.AddRating
{
    // Command carries the data needed to perform the action
    // IRequest<bool> means MediatR will route this to a handler returning bool
    public class AddRatingCommand : IRequest<bool>
    {
        public int Stars { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
        public string UserEmail { get; set; }
    }
}