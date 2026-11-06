
using Ecom.API.Helper;
using Ecom.Application.Features.Auth.Commands.ActiveAccount;
using Ecom.Application.Features.Auth.Commands.ForgetPassword;
using Ecom.Application.Features.Auth.Commands.Login;
using Ecom.Application.Features.Auth.Commands.Register;
using Ecom.Application.Features.Auth.Commands.ResetPassword;
using Ecom.Application.Features.Auth.Commands.UpdateAddress;
using Ecom.Application.Features.Auth.Queries.GetUserAddress;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result == "done"
                ? Ok(new ResponseAPI(200, "Registration successful. Please check your email to activate your account."))
                : BadRequest(new ResponseAPI(400, result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            // Any failure message starts with "Please"
            if (result.StartsWith("Please"))
                return BadRequest(new ResponseAPI(400, result));

            // Set JWT in HttpOnly cookie
            Response.Cookies.Append("token", result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Expires = DateTime.UtcNow.AddDays(1),
            });

            return Ok(new ResponseAPI(200, result));
        }

        
        [HttpPost("active-account")]
        public async Task<IActionResult> ActiveAccount([FromBody] ActiveAccountCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result
                ? Ok(new ResponseAPI(200, "Account activated successfully"))
                : BadRequest(new ResponseAPI(400, "Activation failed. A new activation link has been sent to your email"));
        }

        
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
          
            return Ok(new ResponseAPI(200, "If that email is registered, a reset link has been sent"));
        }

        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result == "done"
                ? Ok(new ResponseAPI(200, "Password reset successfully"))
                : BadRequest(new ResponseAPI(400, result));
        }

      
        [Authorize]
        [HttpPut("update-address")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            command.UserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _mediator.Send(command, cancellationToken);
            return result
                ? Ok(new ResponseAPI(200, "Address updated successfully"))
                : BadRequest(new ResponseAPI(400, "Failed to update address"));
        }

        
        [Authorize]
        [HttpGet("get-address")]
        public async Task<IActionResult> GetAddress(CancellationToken cancellationToken)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var query = new GetUserAddressQuery(email);
            var result = await _mediator.Send(query, cancellationToken);
            return result is not null
                ? Ok(result)
                : NotFound(new ResponseAPI(404, "No address found for this user"));
        }

      
        [Authorize]
        [HttpGet("get-user-name")]
        public IActionResult GetUserName()
        {
            return Ok(new ResponseAPI(200, User.Identity?.Name));
        }

    
        [HttpGet("is-auth")]
        public IActionResult IsUserAuth()
        {
            return User.Identity?.IsAuthenticated == true
                ? Ok(new ResponseAPI(200, "Authenticated"))
                : Unauthorized(new ResponseAPI(401, "Not authenticated"));
        }

        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
          
            Response.Cookies.Append("token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Expires = DateTime.UtcNow.AddDays(-1),
            });

            return Ok(new ResponseAPI(200, "Logged out successfully"));
        }
    }
}