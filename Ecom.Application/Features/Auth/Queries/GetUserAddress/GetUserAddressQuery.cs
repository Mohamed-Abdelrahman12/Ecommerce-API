using Ecom.Application.DTOs;
using MediatR;

namespace Ecom.Application.Features.Auth.Queries.GetUserAddress
{
    public class GetUserAddressQuery : IRequest<ShippingAddressDTO>
    {
        public string UserEmail { get; set; }

        public GetUserAddressQuery(string userEmail)
        {
            UserEmail = userEmail;
        }
    }
}
