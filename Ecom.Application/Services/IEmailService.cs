

using Ecom.Application.DTOs;

namespace Ecom.Core.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailDTO emailDTO);
    }
}
