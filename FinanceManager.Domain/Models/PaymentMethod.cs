using System.ComponentModel.DataAnnotations;


namespace FinanceManager.Domain.Models
{
    public class PaymentMethod
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
       
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        public bool? IsActive { get; set; } = true;
    }
}
