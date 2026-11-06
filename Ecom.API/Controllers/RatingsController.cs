
using Ecom.API.Helper;
using Ecom.Application.Features.Ratings.Commands.AddRating;
using Ecom.Application.Features.Ratings.Commands.DeleteRating;
using Ecom.Application.Features.Ratings.Commands.UpdateRating;
using Ecom.Application.Features.Ratings.Dto;
using Ecom.Application.Features.Ratings.Queries.GetAllRatings;
using Ecom.Application.Features.Ratings.Queries.GetRatings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
      
        private readonly IMediator _mediator;

        public RatingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

     
        [HttpGet("get-rating/{productId}")]
        public async Task<IActionResult> GetRatings(int productId, CancellationToken cancellationToken)
        {
            var query = new GetRatingsQuery(productId);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("add-rating")]
        public async Task<IActionResult> AddRating([FromBody] RatingDTO dto, CancellationToken cancellationToken)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                return Unauthorized(new ResponseAPI(401, "User is not authenticated"));

            var command = new AddRatingCommand
            {
                Stars = dto.stars,
                Content = dto.content,
                ProductId = dto.productId,
                UserEmail = email,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return result
                ? Ok(new ResponseAPI(200, "Rating added successfully"))
                : BadRequest(new ResponseAPI(400, "You have already rated this product"));
        }

        
        [Authorize]
        [HttpPut("update-rating")]
        public async Task<IActionResult> UpdateRating([FromBody] UpdateRatingDTO dto, CancellationToken cancellationToken)
        {
            var command = new UpdateRatingCommand
            {
                Id = dto.id,
                Stars = dto.stars,
                Content = dto.content,
                ProductId = dto.productId,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return result
                ? Ok(new ResponseAPI(200, "Rating updated successfully"))
                : NotFound(new ResponseAPI(404, "Rating not found"));
        }

        
        [Authorize]
        [HttpDelete("delete-rating/{ratingId}")]
        public async Task<IActionResult> DeleteRating(int ratingId, CancellationToken cancellationToken)
        {
            var command = new DeleteRatingCommand(ratingId);
            var result = await _mediator.Send(command, cancellationToken);
            return result
                ? Ok(new ResponseAPI(200, "Rating deleted successfully"))
                : NotFound(new ResponseAPI(404, "Rating not found"));
        }
       
        [AllowAnonymous]
        [HttpGet("all-rating")]
        public async Task<IActionResult> GetAllRatings(CancellationToken cancellationToken)
        {
            var query = new GetAllRatingsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
