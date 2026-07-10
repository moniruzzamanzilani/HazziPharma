using HazziPharma.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.ViewModels
{
    public class SalesReportViewModel
    {
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        public List<Sale> Sales { get; set; } = new();

        public decimal TotalSalesAmount { get; set; }
    }
}