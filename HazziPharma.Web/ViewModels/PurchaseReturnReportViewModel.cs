using HazziPharma.Web.Models;

namespace HazziPharma.Web.ViewModels
{
    public class PurchaseReturnReportViewModel
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<PurchaseReturn> PurchaseReturns { get; set; }
            = new();

        public decimal TotalReturnAmount { get; set; }
    }
}