using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Contacts.Commands.MarkAsReadMessage
{
    public class MarkAsReadCommandHandler: IRequestHandler<MarkAsReadCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkAsReadCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<bool> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.ContactRepository.GetByIdAsync(request.Id, cancellationToken);
            if (message == null) return false;

            message.IsRead = true;
            await _unitOfWork.ContactRepository.UpdateAsync(message, cancellationToken);
            //await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
