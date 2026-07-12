using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }
    }
}