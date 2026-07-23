using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class PurchaseViewModel
    {
        public string PurchaseNo { get; set; } = "";

        [Required(ErrorMessage = "Supplier is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a supplier.")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Purchase date is required.")]
        public DateTime PurchaseDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Invoice No is required.")]
        public string? InvoiceNo { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal DueAmount { get; set; }

        public List<SelectListItem> Suppliers { get; set; } = new();

        public List<PurchaseDetailViewModel> Items { get; set; } = new();
    }
}