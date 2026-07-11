using HazziPharma.Web.Models;

namespace HazziPharma.Web.ViewModels
{
    public class ExpiryReportViewModel
    {
        public List<PurchaseDetail> Medicines { get; set; } = new();

        public int TotalMedicines { get; set; }

        public int ExpiredCount { get; set; }

        public int ExpiringSoonCount { get; set; }
    }
}