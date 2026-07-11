using HazziPharma.Web.Models;

namespace HazziPharma.Web.ViewModels
{
    public class StockReportViewModel
    {
        public List<Product> Products { get; set; } = new();

        public int TotalMedicines { get; set; }

        public int LowStockCount { get; set; }

        public int OutOfStockCount { get; set; }
    }
}