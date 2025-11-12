namespace FinanceManager.Application.Features.ApplicationUsers.Dtos
{
    public class ApplicationUserResponseDto
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }   
        public required string LastName { get; set; }    
        public required string Address { get; set; }   
        public List<string> Roles { get; set; } = new List<string>();

        public bool IsLocked { get; set; }

        public bool IsManuallyLocked { get; set; }

        public string ?LockReason { get; set; }
    }
}
