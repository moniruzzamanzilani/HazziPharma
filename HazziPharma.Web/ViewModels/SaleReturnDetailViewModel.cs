using Microsoft.AspNetCore.Mvc.Rendering;

namespace HazziPharma.Web.ViewModels
{
    public class SaleReturnDetailViewModel
    {
        public int ProductId { get; set; }

        public decimal ReturnPrice { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public List<SelectListItem> Products { get; set; } = new();
    }
}