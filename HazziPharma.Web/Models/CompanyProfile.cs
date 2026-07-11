using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class CompanyProfile
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string CompanyName { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Address { get; set; }

        [StringLength(30)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? Website { get; set; }

        [StringLength(50)]
        public string? TradeLicense { get; set; }

        [StringLength(50)]
        public string? BIN { get; set; }

        public string? LogoPath { get; set; }
    }
}