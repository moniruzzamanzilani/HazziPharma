using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class PurchaseReturnViewModel
    {
        public string ReturnNo { get; set; } = "";

        [Required]
        public int PurchaseId { get; set; }

        public DateTime ReturnDate { get; set; } = DateTime.Today;

        public string? Remarks { get; set; }

        public decimal TotalAmount { get; set; }

        public List<SelectListItem> Purchases { get; set; } = new();

        public List<PurchaseReturnDetailViewModel> Items { get; set; } = new();
    }
}