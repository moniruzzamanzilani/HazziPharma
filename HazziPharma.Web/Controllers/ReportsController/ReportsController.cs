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
        
        [HttpGet]
        public async Task<IActionResult> SalesReport(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Sales.AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(x => x.SaleDate.Date >= fromDate.Value.Date);
            }

            if (toDate.HasValue)
            {
                query = query.Where(x => x.SaleDate.Date <= toDate.Value.Date);
            }

            var sales = await query
                .OrderByDescending(x => x.SaleDate)
                .ToListAsync();

            var model = new SalesReportViewModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                Sales = sales,
                TotalSalesAmount = sales.Sum(x => x.TotalAmount)
            };

            return View(model);
        }
    } 
}
