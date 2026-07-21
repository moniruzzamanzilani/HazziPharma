using Microsoft.AspNetCore.Mvc.Rendering;
namespace HazziPharma.Web.ViewModels
{
    public class SupplierLedgerViewModel
    {
        public DateTime Date { get; set; }

        public string Type { get; set; } = string.Empty;

        public string VoucherNo { get; set; } = string.Empty;

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal Balance { get; set; }
        public int SupplierId { get; set; }

        public List<SelectListItem> Suppliers { get; set; } = new();
        public List<SupplierLedgerViewModel> Ledger { get; set; } = new();
    }
}