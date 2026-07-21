using HazziPharma.Web.Data;
using HazziPharma.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Controllers
{
    [Authorize]
    public class SupplierLedgersController : Controller
    {
        private readonly HazziPharmaDbContext _context;

        public SupplierLedgersController(HazziPharmaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? supplierId)
        {
            var model = new SupplierLedgerViewModel();

            model.SupplierId = supplierId ?? 0;

            model.Suppliers = await _context.Suppliers
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToListAsync();

            if (!supplierId.HasValue)
                return View(model);
            var purchases = await _context.Purchases
            .Where(x => x.SupplierId == supplierId)
            .Select(x => new SupplierLedgerViewModel
            {
                Date = x.PurchaseDate,
                Type = "Purchase",
                VoucherNo = x.PurchaseNo,
                Debit = x.TotalAmount,
                Credit = 0,
                Balance = 0
            })
            .ToListAsync();
            var purchaseReturns = await _context.PurchaseReturns
            .Where(x => x.Purchase!.SupplierId == supplierId)
            .Select(x => new SupplierLedgerViewModel
            {
                Date = x.ReturnDate,
                Type = "Purchase Return",
                VoucherNo = x.ReturnNo,
                Debit = 0,
                Credit = x.TotalAmount,
                Balance = 0
            })
            .ToListAsync();

            model.Ledger = purchases
                 .Concat(purchaseReturns)
                 .OrderBy(x => x.Date)
                 .ToList();

            decimal balance = 0;

            foreach (var item in model.Ledger)
            {
                balance += item.Debit;
                balance -= item.Credit;

                item.Balance = balance;
            }

            return View(model);
        }
    }
}