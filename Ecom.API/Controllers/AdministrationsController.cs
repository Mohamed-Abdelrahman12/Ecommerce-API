
using Ecom.Application.Features.Roles.AdminstrationDTO;
using Ecom.Application.Features.Roles.Commands.AddRole;
using Ecom.Application.Features.Roles.Commands.DeleteRole;
using Ecom.Application.Features.Roles.Commands.UpdateRole;
using Ecom.Application.Features.Roles.Queries.GetAllRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministrationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministrationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll-Roles")]
        public async Task<IActionResult> Get()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpPost("Add-Role")]
        public async Task<IActionResult> Add([FromBody] AddRoleDto role)
        {
            if (role == null || !ModelState.IsValid)
            {
                return BadRequest("Can't Add Role");
            }

            var command = new AddRoleCommand(role.RoleName);
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new AddRoleResponseDto { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPut("Update-Role")]
        public async Task<IActionResult> Update([FromBody] RolesDto Currole)
        {
            var command = new UpdateRoleCommand(Currole.Id, Currole.RoleName);
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new AddRoleResponseDto { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpDelete("Delete-Role")]
        public async Task<IActionResult> Delete(string RoleId)
        {
            var command = new DeleteRoleCommand(RoleId);
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new AddRoleResponseDto { Errors = errors });
            }

            return StatusCode(202);
        }
    }
}