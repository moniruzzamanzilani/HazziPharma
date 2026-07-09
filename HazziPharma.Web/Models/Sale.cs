using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public string SaleNo { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public string? InvoiceNo { get; set; }

        public string? Remarks { get; set; }

        // Navigation
        public ICollection<SaleDetail> SaleDetails { get; set; }
            = new List<SaleDetail>();
    }
}