using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class SaleDetailViewModel
    {
        [Required(ErrorMessage = "Please select a medicine.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a medicine.")]
        public int ProductId { get; set; }

        public List<SelectListItem> Products { get; set; } = new();

        public decimal? Stock { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Sale Price is required.")]
        [Range(typeof(decimal), "0.01", "999999999",
            ErrorMessage = "Sale Price must be greater than zero.")]
        public decimal? SalePrice { get; set; }

        [Range(typeof(decimal), "0", "999999999",
            ErrorMessage = "Discount cannot be negative.")]
        public decimal? Discount { get; set; }

        public decimal? SubTotal { get; set; }
    }
}