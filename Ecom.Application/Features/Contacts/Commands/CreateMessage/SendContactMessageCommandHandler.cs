using AutoMapper;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using MediatR;

namespace Ecom.Application.Features.Contacts.Commands.CreateMessage
{
    public class SendContactMessageCommandHandler : IRequestHandler<SendContactMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SendContactMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SendContactMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<ContactMessage>(request.ContactDto);

            await _unitOfWork.ContactRepository.AddAsync(message, cancellationToken);
            //await _unitOfWork.SaveChangesAsync(); // Ensure your UoW has this method

            return true;
        }
    }
}
