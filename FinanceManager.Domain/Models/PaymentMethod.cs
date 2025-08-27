using System.ComponentModel.DataAnnotations;


namespace FinanceManager.Domain.Models
{
    public class PaymentMethod
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; } = true;
    }
}
