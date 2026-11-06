using Ecom.Application.Features.Auth.Dto;
using Ecom.Domain.Entities;

namespace Ecom.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(RegisterDTO registerDTO, CancellationToken cancellationToken = default);
        Task<string> LoginAsync(LoginDTO loginDTO, CancellationToken cancellationToken = default);
        Task<bool> SendEmailForForgetPasswordAsync(string email, CancellationToken cancellationToken = default);
        Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO, CancellationToken cancellationToken = default);
        Task<bool> ActiveAccountAsync(ActiveAccountDTO accountDTO, CancellationToken cancellationToken = default);
        Task<bool> UpdateAddressAsync(string email, Address address, CancellationToken cancellationToken = default);
        Task<Address> GetUserAddressAsync(string email, CancellationToken cancellationToken = default);
    }



}

