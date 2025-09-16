

namespace FinanceManager.Application.Dtos.ApplicationUser
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
