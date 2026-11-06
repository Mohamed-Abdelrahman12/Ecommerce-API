using MediatR;


namespace Ecom.Application.Features.Auth.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<bool>
    {
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
