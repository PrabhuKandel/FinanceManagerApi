

namespace FinanceManager.Application.Dtos.ApplicationUser
{
    public class ApplicationUserResponseDto
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }   
        public required string LastName { get; set; }    
        public required string Address { get; set; }   
        public List<string> Roles { get; set; } = new List<string>();
    }
}
