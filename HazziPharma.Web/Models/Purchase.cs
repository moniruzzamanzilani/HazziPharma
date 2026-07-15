using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public string PurchaseNo { get; set; } = "";

        [Required]
        public int SupplierId { get; set; }

        public Supplier? Supplier { get; set; }

        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; } = DateTime.Today;

        public string? InvoiceNo { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal DueAmount { get; set; }

        public string? Remarks { get; set; }
        public ICollection<PurchaseDetail>? PurchaseDetails { get; set; }
    }
}