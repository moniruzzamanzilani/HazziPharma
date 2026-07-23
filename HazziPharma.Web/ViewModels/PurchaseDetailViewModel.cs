using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class PurchaseDetailViewModel
    {
        [Required(ErrorMessage = "Please select a medicine.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a medicine.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Purchase Price is required.")]
        [Range(typeof(decimal), "0.01", "999999999",
         ErrorMessage = "Purchase Price must be greater than zero.")]
        public decimal PurchasePrice { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Batch No is required.")]
        public string? BatchNo { get; set; }

        [Required(ErrorMessage = "Expiry date is required.")]
        public DateTime? ExpiryDate { get; set; }

        public decimal SubTotal { get; set; }

        public List<SelectListItem> Products { get; set; } = new();
    }
}