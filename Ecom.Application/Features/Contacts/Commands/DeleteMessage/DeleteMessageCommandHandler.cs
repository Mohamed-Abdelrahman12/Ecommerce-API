using Ecom.Application.Interfaces;
using MediatR;


namespace Ecom.Application.Features.Contacts.Commands.DeleteMessage
{
    internal class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteMessageCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.ContactRepository.GetByIdAsync(request.Id, cancellationToken);

            if (message == null)
                return false;

            await _unitOfWork.ContactRepository.DeleteAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
