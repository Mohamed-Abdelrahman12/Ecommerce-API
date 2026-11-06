using MediatR;

namespace Ecom.Application.Features.Contacts.Commands.MarkAsReadMessage
{
    public record MarkAsReadCommand(int Id) : IRequest<bool>;
}
