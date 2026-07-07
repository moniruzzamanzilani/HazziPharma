using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }
    }
}