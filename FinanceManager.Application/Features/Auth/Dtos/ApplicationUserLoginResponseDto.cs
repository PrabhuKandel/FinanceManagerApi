

namespace FinanceManager.Application.Features.Auth.Dtos
{
    public class ApplicationUserLoginResponseDto
    {
        public required string UserId { get; set; }
        public  required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
