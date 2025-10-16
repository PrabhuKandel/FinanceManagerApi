

namespace FinanceManager.Domain.Entities
{
    public class Permission
    {
        public Guid Id { get; set; }=Guid.NewGuid();
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
