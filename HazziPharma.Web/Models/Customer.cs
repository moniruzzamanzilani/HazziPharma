using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        public decimal PreviousDue { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }
}