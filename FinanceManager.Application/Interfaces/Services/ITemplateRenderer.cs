
namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITemplateRenderer
    {
        Task<string> RenderTemplateAsync(string templatePath, object data);
    }
}
