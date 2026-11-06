
using Ecom.API.Helper;
using Ecom.Application.Features.WishList.Commands.AddToWishList;
using Ecom.Application.Features.WishList.Commands.DeleteWishList;
using Ecom.Application.Features.WishList.Dto;
using Ecom.Application.Features.WishList.Queries.GetWishList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WishListController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllWishList(CancellationToken cancellationToken)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                return Unauthorized(new ResponseAPI(401, "User is not authenticated"));

            var query = new GetWishListQuery(email);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddToWishList([FromBody] AddToWishListDTO dto, CancellationToken cancellationToken)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                return Unauthorized(new ResponseAPI(401, "User is not authenticated"));

            var command = new AddToWishListCommand
            {
                ProductId = dto.ProductId,
                UserEmail = email,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return result
                ? Ok(new ResponseAPI(200, "Product added to wishlist successfully"))
                : BadRequest(new ResponseAPI(400, "Product not found or already in your wishlist"));
        }

  
        [HttpDelete("delete/{wishListId}")]
        public async Task<IActionResult> DeleteFromWishList(int wishListId, CancellationToken cancellationToken)
        {
            var command = new DeleteWishListCommand(wishListId);
            var result = await _mediator.Send(command, cancellationToken);
            return result
                ? Ok(new ResponseAPI(200, "Product removed from wishlist successfully"))
                : NotFound(new ResponseAPI(404, "Wishlist item not found"));
        }
    }
}