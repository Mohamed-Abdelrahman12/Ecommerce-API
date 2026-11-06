
using Microsoft.AspNetCore.Http;

namespace Ecom.Core.Services
{
    public interface IImageManagementService
    {
        Task<string> AddImageAsync(IFormFile file, string src);
        Task<List<string>> AddImageAsync(IFormFileCollection files, string src);
        void DeleteImageAsync( string src);
    }
}
