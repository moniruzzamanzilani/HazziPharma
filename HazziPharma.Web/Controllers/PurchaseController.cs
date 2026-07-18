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
    public class PurchaseController : Controller
    {
        private readonly HazziPharmaDbContext _context;
        private readonly NumberGeneratorService _numberGenerator;

        public PurchaseController(
    HazziPharmaDbContext context,
    NumberGeneratorService numberGenerator)
        {
            _context = context;
            _numberGenerator = numberGenerator;
        }
        // =========================
        // GET: Purchase
        // =========================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var purchases = await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseDetails)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return View(purchases);
        }

        public async Task<IActionResult> Create()
        {
            var model = new PurchaseViewModel();
            model.PurchaseNo = await _numberGenerator.GeneratePurchaseNoAsync();
            model.InvoiceNo = await _numberGenerator.GeneratePurchaseInvoiceNoAsync();

            model.Suppliers = await _context.Suppliers
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            model.Items.Add(new PurchaseDetailViewModel
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
        // new Method Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseViewModel model)
        {
           
            if (!ModelState.IsValid)
            {
                var errors = ModelState
      .Where(x => x.Value!.Errors.Count > 0)
      .Select(x => $"{x.Key} => {string.Join(", ", x.Value!.Errors.Select(e => e.ErrorMessage))}");

                return Content(string.Join("\n", errors));
                model.Suppliers = await _context.Suppliers
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    })
                    .ToListAsync();

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
               
                    foreach (var error in ModelState)
                    {
                        foreach (var item in error.Value.Errors)
                        {
                            Console.WriteLine($"{error.Key} => {item.ErrorMessage}");
                        }
                    }

                    // Suppliers Load...
                    // Products Load...

                    return View(model);
               
            }

            // Purchase Header
            var purchase = new Purchase
            {
                PurchaseNo = model.PurchaseNo,
                SupplierId = model.SupplierId,
                PurchaseDate = model.PurchaseDate,
                InvoiceNo = model.InvoiceNo,
                Remarks = model.Remarks,

                TotalAmount = model.TotalAmount,
                Discount = model.Discount,
                PaidAmount = model.PaidAmount,
                DueAmount = model.DueAmount
            };

            _context.Purchases.Add(purchase);

            await _context.SaveChangesAsync();

            // Purchase Detail
            // Purchase Details Save + Stock Update
            foreach (var item in model.Items)
            {
         
                var detail = new PurchaseDetail
                {
                    PurchaseId = purchase.Id,
                    ProductId = item.ProductId,
                    PurchasePrice = item.PurchasePrice,
                    Quantity = item.Quantity,
                    BatchNo = item.BatchNo,
                    ExpiryDate = item.ExpiryDate,
                    SubTotal = item.SubTotal
                };

                _context.PurchaseDetails.Add(detail);

                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product != null)
                {
                    product.Stock += item.Quantity;
                }
            }

            await _context.SaveChangesAsync();

            return Content("Purchase Saved Successfully");
        }
    }
}