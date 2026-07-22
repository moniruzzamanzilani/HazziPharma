using HazziPharma.Web.Models;

namespace HazziPharma.Web.ViewModels
{
    public class SaleReturnReportViewModel
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<SaleReturn> SaleReturns { get; set; }
            = new();

        public decimal TotalReturnAmount { get; set; }
    }
}