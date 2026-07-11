using HazziPharma.Web.Data;
using HazziPharma.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly HazziPharmaDbContext _context;

        public ReportsController(HazziPharmaDbContext context)
        {
            _context = context;
        }

        // ==========================
        // Stock Report
        // ==========================
        [HttpGet]
        public async Task<IActionResult> StockReport()
        {
            var model = new StockReportViewModel
            {
                Products = await _context.Products
                    .Include(p => p.Generic)
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .OrderBy(p => p.Name)
                    .ToListAsync(),

                TotalMedicines = await _context.Products.CountAsync(),

                LowStockCount = await _context.Products
                    .CountAsync(p => p.Stock > 0 && p.Stock <= p.ReorderLevel),

                OutOfStockCount = await _context.Products
                    .CountAsync(p => p.Stock == 0)
            };

            return View(model);
        }

        // ==========================
        // Expiry Report
        // ==========================
        [HttpGet]
        public async Task<IActionResult> ExpiryReport()
        {
            var medicines = await _context.PurchaseDetails
                .Include(x => x.Product)
                .Include(x => x.Purchase)
                    .ThenInclude(p => p.Supplier)
                .Where(x => x.ExpiryDate.HasValue)
                .OrderBy(x => x.ExpiryDate)
                .ToListAsync();

            var model = new ExpiryReportViewModel
            {
                Medicines = medicines,

                TotalMedicines = medicines.Count,

                ExpiredCount = medicines.Count(x =>
                    x.ExpiryDate!.Value.Date < DateTime.Today),

                ExpiringSoonCount = medicines.Count(x =>
                    x.ExpiryDate!.Value.Date >= DateTime.Today &&
                    x.ExpiryDate!.Value.Date <= DateTime.Today.AddDays(30))
            };

            return View(model);
        }
    } 
}
