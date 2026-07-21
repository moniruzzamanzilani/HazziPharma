using HazziPharma.Web.Data;
using HazziPharma.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Controllers
{
    [Authorize]
    public class CustomerLedgersController : Controller
    {
        private readonly HazziPharmaDbContext _context;

        public CustomerLedgersController(HazziPharmaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? customerId)
        {
            var model = new CustomerLedgerViewModel();

            model.CustomerId = customerId ?? 0;

            model.Customers = await _context.Customers
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToListAsync();

            if (!customerId.HasValue)
                return View(model);
            var sales = await _context.Sales
                .Where(x => x.CustomerId == customerId)
                .Select(x => new CustomerLedgerViewModel
                {
                    Date = x.SaleDate,
                    Type = "Sale",
                    VoucherNo = x.SaleNo,
                    Debit = x.TotalAmount,
                    Credit = 0,
                    Balance = 0
                })
                .ToListAsync();
            var saleReturns = await _context.SaleReturns
                .Where(x => x.Sale!.CustomerId == customerId)
                .Select(x => new CustomerLedgerViewModel
                {
                    Date = x.ReturnDate,
                    Type = "Sale Return",
                    VoucherNo = x.ReturnNo,
                    Debit = 0,
                    Credit = x.TotalAmount,
                    Balance = 0
                })
                .ToListAsync();

            model.Ledger = sales
                 .Concat(saleReturns)
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