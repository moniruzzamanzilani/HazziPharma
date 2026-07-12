using HazziPharma.Web.Data;
using HazziPharma.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HazziPharmaDbContext _context;

        public DashboardController(HazziPharmaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TotalProducts = await _context.Products.CountAsync(),

                TotalSuppliers = await _context.Suppliers.CountAsync(),

                TotalPurchases = await _context.Purchases.CountAsync(),

                TotalSales = await _context.Sales.CountAsync(),

                TotalPurchaseAmount = await _context.Purchases
                    .SumAsync(x => (decimal?)x.TotalAmount) ?? 0,

                TotalSalesAmount = await _context.Sales
                    .SumAsync(x => (decimal?)x.TotalAmount) ?? 0,

                TotalExpenses = await _context.Expenses.CountAsync(),

                TotalExpenseAmount = await _context.Expenses
                    .SumAsync(x => (decimal?)x.Amount) ?? 0,

                TodayExpenseAmount = await _context.Expenses
                    .Where(x => x.ExpenseDate.Date == DateTime.Today)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0,

                LowStockCount = await _context.Products
                    .CountAsync(p => p.Stock <= p.ReorderLevel),
                
                ExpiredMedicineCount = await _context.PurchaseDetails
                    .CountAsync(x =>
                     x.ExpiryDate.HasValue &&
                     x.ExpiryDate.Value.Date < DateTime.Today),

                ExpiringSoonCount = await _context.PurchaseDetails
                    .CountAsync(x =>
                     x.ExpiryDate.HasValue &&
                     x.ExpiryDate.Value.Date >= DateTime.Today &&
                     x.ExpiryDate.Value.Date <= DateTime.Today.AddDays(30)),
            };

                 model.LowStockProducts = await _context.Products
                     .Where(p => p.Stock <= p.ReorderLevel)
                     .OrderBy(p => p.Stock)
                     .ToListAsync();

            model.ExpiringSoonMedicines = await _context.PurchaseDetails
                .Include(p => p.Product)
                .Where(x =>
                    x.ExpiryDate.HasValue &&
                    x.ExpiryDate.Value.Date >= DateTime.Today &&
                    x.ExpiryDate.Value.Date <= DateTime.Today.AddDays(30))
                .OrderBy(x => x.ExpiryDate)
                .ToListAsync();
            model.TodayPurchaseCount = await _context.Purchases
                        .CountAsync(x => x.PurchaseDate.Date == DateTime.Today);

            model.TodaySalesCount = await _context.Sales
                .CountAsync(x => x.SaleDate.Date == DateTime.Today);

            model.TodayPurchaseAmount = await _context.Purchases
                .Where(x => x.PurchaseDate.Date == DateTime.Today)
                .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;

            model.TodaySalesAmount = await _context.Sales
                .Where(x => x.SaleDate.Date == DateTime.Today)
                .SumAsync(x => (decimal?)x.TotalAmount) ?? 0;
            model.RecentPurchases = await _context.Purchases
                .Include(p => p.Supplier)
                .OrderByDescending(p => p.Id)
                .Take(5)
                .ToListAsync();

            model.RecentSales = await _context.Sales
                .OrderByDescending(s => s.Id)
                .Take(5)
                .ToListAsync();
            model.RecentExpenses = await _context.Expenses
                .Include(x => x.ExpenseCategory)
                .OrderByDescending(x => x.ExpenseDate)
                .Take(5)
                .ToListAsync();
            model.TopSellingProducts = await _context.SaleDetails
                .Include(s => s.Product)
                .GroupBy(s => s.Product!.Name)
                .Select(g => new ProductSalesViewModel
                {
            ProductName = g.Key,
            QuantitySold = g.Sum(x => x.Quantity)
        })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToListAsync();
                        return View(model);
        }
    }
}