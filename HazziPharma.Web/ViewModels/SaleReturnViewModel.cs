using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class SaleReturnViewModel
    {
        public string ReturnNo { get; set; } = "";

        [Required]
        public int SaleId { get; set; }

        public DateTime ReturnDate { get; set; } = DateTime.Today;

        public string? Remarks { get; set; }

        public decimal TotalAmount { get; set; }

        public List<SelectListItem> Sales { get; set; } = new();

        public List<SaleReturnDetailViewModel> Items { get; set; } = new();
    }
}