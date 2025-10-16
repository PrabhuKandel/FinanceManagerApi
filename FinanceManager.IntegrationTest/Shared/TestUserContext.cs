

using FinanceManager.Application.Interfaces.Services;

namespace FinanceManager.IntegrationTest.Shared
{
    internal class TestUserContext : IUserContext
    {
        /// <summary>
        /// Mimics authenticated user's ID
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Mimics user's role ("Admin", "User", etc.)
        /// </summary>
        public string Role { get; set; } = "User";

        /// <summary>
        /// Mimics IsAdmin() from production code
        /// </summary>
        public bool IsAdmin() => Role == "Admin";
    }
}
