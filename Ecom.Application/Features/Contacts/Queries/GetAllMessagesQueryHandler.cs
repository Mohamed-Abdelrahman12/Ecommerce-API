using AutoMapper;
using Ecom.Application.Features.Contacts.Dto;
using Ecom.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Application.Features.Contacts.Queries
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, IReadOnlyList<ContactMessageResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMessagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ContactMessageResponseDTO>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _unitOfWork.ContactRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ContactMessageResponseDTO>>(messages);
        }
    }
}
