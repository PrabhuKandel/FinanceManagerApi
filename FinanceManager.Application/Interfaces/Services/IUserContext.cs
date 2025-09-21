
namespace FinanceManager.Application.Interfaces.Services
{
    public interface IUserContext
    {
        string UserId { get; }
        string Role { get; }
       
        bool IsAdmin();

    }
}
