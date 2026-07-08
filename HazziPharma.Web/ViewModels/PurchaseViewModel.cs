using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class PurchaseViewModel
    {
        public string PurchaseNo { get; set; } = "";

        [Required]
        public int SupplierId { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Today;

        public string? InvoiceNo { get; set; }

        public string? Remarks { get; set; }

        public List<SelectListItem> Suppliers { get; set; } = new();

        public List<PurchaseDetailViewModel> Items { get; set; } = new();
    }
}