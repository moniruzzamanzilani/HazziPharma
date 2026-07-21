using Microsoft.AspNetCore.Mvc.Rendering;

namespace HazziPharma.Web.ViewModels
{
    public class CustomerLedgerViewModel
    {
        public DateTime Date { get; set; }

        public string Type { get; set; } = string.Empty;

        public string VoucherNo { get; set; } = string.Empty;

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal Balance { get; set; }

        public int CustomerId { get; set; }

        public List<SelectListItem> Customers { get; set; } = new();

        public List<CustomerLedgerViewModel> Ledger { get; set; } = new();
    }
}