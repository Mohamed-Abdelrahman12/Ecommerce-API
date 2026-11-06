using Ecom.Application.Features.Contacts.Dto;
using MediatR;

namespace Ecom.Application.Features.Contacts.Commands.CreateMessage
{
    public record SendContactMessageCommand(SendContactMessageDTO ContactDto) : IRequest<bool>;
}
