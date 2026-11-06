

using Ecom.Application.DTOs;
using Ecom.Application.Features.Auth.Dto;
using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Ecom.Infastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Ecom.infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IGenerateToken _generateToken;
        private readonly AppDbContext _context;

        public AuthRepository(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IGenerateToken generateToken,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _generateToken = generateToken;
            _context = context;
        }

        public async Task<string> RegisterAsync(RegisterDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return "Invalid request";

            if (await _userManager.FindByNameAsync(dto.UserName) is not null)
                return "This username is already registered";

            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                return "This email is already registered";

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                DispalyName = dto.DisplayName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return result.Errors.First().Description;

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            await _userManager.AddToRoleAsync(user, "User");

            // Encode the token so it survives URL transport safely
            string rawToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(rawToken));

            await SendEmailAsync(user.Email, encodedToken, "active", "ActiveEmail",
                "Please activate your email — click the button below");

            return "done";
        }

        public async Task<string> LoginAsync(LoginDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return "Invalid request";

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
                return "Please check your email and password";

            if (!user.EmailConfirmed)
            {
                string rawToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(rawToken));

                await SendEmailAsync(user.Email, encodedToken, "active", "ActiveEmail",
                    "Please activate your email — click the button below");

                return "Please confirm your email first. We have sent an activation link to your email";
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);

            if (signInResult.Succeeded)
                return _generateToken.GetAndGenerateToken(user);

            if (signInResult.IsLockedOut)
                return "Account is locked. Please try again later";

            return "Please check your email and password";
        }

        public async Task<bool> SendEmailForForgetPasswordAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Encode for safe URL transport
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            await SendEmailAsync(user.Email, encodedToken, "Reset-Password", "Reset-Password",
                "Click the button below to reset your password");

            return true;
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto is null) return "Invalid request";

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null) return "User not found";

            // Decode the token coming back from the frontend
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, dto.Password);

            if (result.Succeeded) return "done";

            return result.Errors.First().Description;
        }

        public async Task<bool> ActiveAccountAsync(ActiveAccountDTO dto, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null) return false;

            // Decode the Base64Url-encoded token from the email link
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded) return true;

            // Confirmation failed — regenerate and resend
            string newRaw = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string newEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(newRaw));

            await SendEmailAsync(user.Email, newEncoded, "active", "ActiveEmail",
                "Please activate your email — click the button below");

            return false;
        }

        public async Task<bool> UpdateAddressAsync(string email, Address address, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return false;

            var existing = await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AppUserId == user.Id, cancellationToken);

            if (existing is null)
            {
                address.AppUserId = user.Id;
                await _context.Addresses.AddAsync(address, cancellationToken);
            }
            else
            {
                address.Id = existing.Id;
                address.AppUserId = existing.AppUserId;
                _context.Addresses.Update(address);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


        public async Task<Address> GetUserAddressAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) return null;

            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.AppUserId == user.Id, cancellationToken);
        }


        private async Task SendEmailAsync(string email, string token, string component, string subject, string message)
        {
            var emailDto = new EmailDTO(
                sendTo: email,
                from: "no-reply@ecom.com",
                subject: subject,
                content: EmailStringBody.Send(email, token, component, message)
            );
            await _emailService.SendEmail(emailDto);
        }
    }
}