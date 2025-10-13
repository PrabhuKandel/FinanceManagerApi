
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Features.TransactionCategories.Dtos
{

    public class TransactionCategoryResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public CategoryType Type { get; set; }
    }
}
