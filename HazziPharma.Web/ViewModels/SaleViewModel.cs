using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class SaleViewModel
    {
        [Required]
        public string SaleNo { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Please select a customer.")]
        public int? CustomerId { get; set; }

        public List<SelectListItem> Customers { get; set; } = new();

        public string? InvoiceNo { get; set; }

        public string? Remarks { get; set; }

        [Range(0.01, double.MaxValue,
         ErrorMessage = "Grand Total must be greater than zero.")]
        public decimal GrandTotal { get; set; }

        public List<SaleDetailViewModel> Items { get; set; } = new();
    }
}