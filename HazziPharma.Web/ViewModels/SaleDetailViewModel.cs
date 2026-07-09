using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class SaleDetailViewModel
    {
        [Required]
        public int ProductId { get; set; }

        public List<SelectListItem> Products { get; set; } = new();

        public decimal? Stock { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal? SalePrice { get; set; }

        public decimal? Discount { get; set; }

        public decimal? SubTotal { get; set; }
    }
}