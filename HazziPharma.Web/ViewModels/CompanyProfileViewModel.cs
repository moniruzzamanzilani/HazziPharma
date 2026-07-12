using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class CompanyProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public string? TradeLicense { get; set; }

        public string? BIN { get; set; }

        public string? LogoPath { get; set; }

        // Upload File
        public IFormFile? LogoFile { get; set; }
    }
}