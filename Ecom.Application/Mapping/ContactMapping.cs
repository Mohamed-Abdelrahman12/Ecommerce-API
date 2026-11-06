using AutoMapper;
using Ecom.Application.Features.Contacts.Dto;
using Ecom.Domain.Entities;

namespace Ecom.Application.Mapping
{
    public class ContactMapping:Profile
    {
        public ContactMapping()
        {
            CreateMap<SendContactMessageDTO, ContactMessage>().ReverseMap();
            CreateMap<ContactMessage, ContactMessageResponseDTO>().ReverseMap();
        }
    }
}
