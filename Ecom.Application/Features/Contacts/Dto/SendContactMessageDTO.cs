namespace Ecom.Application.Features.Contacts.Dto
{
    public record SendContactMessageDTO(string Name, string Email, string Subject, string Message);
    public record ContactMessageResponseDTO(
        int Id,
        string Name,
        string Email,
        string Subject,
        string Message,
        bool IsRead,
        DateTime CreatedAt
    );
}
