using Microsoft.AspNetCore.Authorization;
using HazziPharma.Web.Data;
using HazziPharma.Web.Models;
using HazziPharma.Web.ViewModels;
using HazziPharma.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly HazziPharmaDbContext _context;
        private readonly NumberGeneratorService _numberGenerator;

        public SalesController(
            HazziPharmaDbContext context,
            NumberGeneratorService numberGenerator)
        {
            _context = context;
            _numberGenerator = numberGenerator;
        }
        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            var sales = await _context.Sales
            .Include(s => s.SaleDetails)
            .OrderByDescending(s => s.Id)
            .ToListAsync();

            return View(sales);
        }
        // =========================
        // GET: Sales/Create
        // =========================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SaleViewModel();
            model.SaleNo = await _numberGenerator.GenerateSaleNoAsync();
            model.InvoiceNo = await _numberGenerator.GenerateInvoiceNoAsync();
            model.Customers = await _context.Customers
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToListAsync();
            model.Items.Add(new SaleDetailViewModel
            {
                Products = await _context.Products
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    })
                    .ToListAsync()

            });

            return View(model);
        }
        // =========================
        // GET: Sales
        // =========================

        [HttpGet]
        public async Task<IActionResult> GetProductInfo(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    stock = p.Stock,
                    salePrice = p.SalePrice
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound();

            return Json(product);
        }
        // =========================
        // GET: Sales/Details/5
        // =========================
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }
        // =========================
        // GET: Sales/Print/5
        // =========================
        [HttpGet]
        public async Task<IActionResult> Print(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }
        // =========================
        // POST: Sales/Create
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleViewModel model, string action)
        {
            // ===== DEBUG START =====
            if (!ModelState.IsValid)
            {
                foreach (var item in model.Items)
                {
                    item.Products = await _context.Products
                        .Select(p => new SelectListItem
                        {
                            Value = p.Id.ToString(),
                            Text = p.Name
                        })
                        .ToListAsync();
                }
                model.Customers = await _context.Customers
                        .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Name
                        })
                        .ToListAsync();
                return View(model);
            }
            // ===== DEBUG END =====

            // Stock Validation
            foreach (var item in model.Items)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product == null)
                {
                    ModelState.AddModelError("", "Product not found.");
                }
                else if (product.Stock < item.Quantity)
                {
                    ModelState.AddModelError("",
                         $"Insufficient stock for {product.Name}. Requested: {item.Quantity}, Available: {product.Stock}.");
                }
            }
              var duplicateProducts = model.Items
                        .GroupBy(x => x.ProductId)
                        .Where(g => g.Key > 0 && g.Count() > 1)
                        .ToList();

                        if (duplicateProducts.Any())
                        {
                            ModelState.AddModelError("", "Same medicine cannot be added multiple times.");
                        }
             
            if (!ModelState.IsValid)
            {
                foreach (var item in model.Items)
                {
                    item.Products = await _context.Products
                        .Select(p => new SelectListItem
                        {
                            Value = p.Id.ToString(),
                            Text = p.Name
                        })
                        .ToListAsync();
                }
                
                model.Customers = await _context.Customers
                        .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Name
                        })
                        .ToListAsync();
                return View(model);
            }

            // Sale Header
            var sale = new Sale
            {
                SaleNo = model.SaleNo,
                SaleDate = model.SaleDate,
                CustomerId = model.CustomerId,
                InvoiceNo = model.InvoiceNo,
                Remarks = model.Remarks,
                TotalAmount = model.Items.Sum(x => x.SubTotal ?? 0)
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            // Sale Details
            foreach (var item in model.Items)
            {
                var detail = new SaleDetail
                {
                    SaleId = sale.Id,
                    ProductId = item.ProductId,
                    SalePrice = item.SalePrice ?? 0,
                    Quantity = item.Quantity,
                    Discount = item.Discount ?? 0,
                    SubTotal = item.SubTotal ?? 0
                };

                _context.SaleDetails.Add(detail);

                var product = await _context.Products
                    .FirstAsync(p => p.Id == item.ProductId);

                product.Stock -= item.Quantity;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Sale Saved Successfully.";

            if (action == "print")
            {
                return RedirectToAction(nameof(Print), new { id = sale.Id });
            }

            return RedirectToAction(nameof(Index));

         
        }
    }
}