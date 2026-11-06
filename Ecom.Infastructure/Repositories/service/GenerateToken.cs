using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecom.infrastructure.Repositories.service
{
    public class GenerateToken : IGenerateToken
    {
        private readonly IConfiguration _configuration;

        public GenerateToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetAndGenerateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var Security = _configuration["Token:Secret"];
            var key = Encoding.ASCII.GetBytes(Security);

            SigningCredentials credentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires =DateTime.Now.AddDays(1),
                Issuer = _configuration["Token:Issure"],
                SigningCredentials = credentials,
                NotBefore = DateTime.Now

            };
            JwtSecurityTokenHandler handler = new();

            var token = handler.CreateToken(tokenDescriptor);

            return  handler.WriteToken(token);
        }
    }
}
