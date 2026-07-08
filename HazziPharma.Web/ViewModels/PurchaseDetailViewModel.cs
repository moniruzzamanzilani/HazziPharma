using Microsoft.AspNetCore.Mvc.Rendering;

namespace HazziPharma.Web.ViewModels
{
    public class PurchaseDetailViewModel
    {
        public int ProductId { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public string? BatchNo { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public decimal SubTotal { get; set; }

        public List<SelectListItem> Products { get; set; } = new();
    }
}