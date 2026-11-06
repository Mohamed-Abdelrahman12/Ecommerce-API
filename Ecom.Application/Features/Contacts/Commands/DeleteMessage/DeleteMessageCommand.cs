using MediatR;

namespace Ecom.Application.Features.Contacts.Commands.DeleteMessage
{
    public record DeleteMessageCommand(int Id) : IRequest<bool>;
}
