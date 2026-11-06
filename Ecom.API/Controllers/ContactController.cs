using Ecom.API.Helper;
using Ecom.Application.Features.Contacts.Commands.DeleteMessage;
using Ecom.Application.Features.Contacts.Commands.MarkAsReadMessage;
using Ecom.Application.Features.Contacts.Queries;
using Ecom.Application.Features.Contacts.Commands.CreateMessage;
using Ecom.Application.Features.Contacts.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Send-Message")]
        public async Task<IActionResult> SendMessage([FromBody] SendContactMessageDTO contactDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseAPI(400, "Invalid data"));

            var result = await _mediator.Send(new SendContactMessageCommand(contactDto));

            if (!result)
                return BadRequest(new ResponseAPI(400, "Could not send message"));

            return Ok(new ResponseAPI(200, "Your message has been sent successfully"));
        }

        
        [HttpGet("All-Message")]
       
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllMessagesQuery());
            return Ok(result);
        }

       
        [HttpPut("Mark-As-Read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _mediator.Send(new MarkAsReadCommand(id));
            return result ? Ok() : NotFound();
        }

      
        [HttpDelete("Delete-Message/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteMessageCommand(id));
            return result ? Ok(new ResponseAPI(200, "Deleted")) : NotFound();
        }
    }
}
