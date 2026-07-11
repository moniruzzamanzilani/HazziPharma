using HazziPharma.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class PurchaseReportViewModel
    {
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.Today;

        public List<Purchase> Purchases { get; set; } = new();

        public decimal TotalPurchaseAmount { get; set; }
    }
}