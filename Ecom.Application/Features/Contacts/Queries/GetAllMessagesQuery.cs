using Ecom.Application.Features.Contacts.Dto;
using MediatR;

namespace Ecom.Application.Features.Contacts.Queries
{
    public record GetAllMessagesQuery() : IRequest<IReadOnlyList<ContactMessageResponseDTO>>;
}
