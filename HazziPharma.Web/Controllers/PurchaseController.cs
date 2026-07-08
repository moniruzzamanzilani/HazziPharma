using HazziPharma.Web.Data;
using HazziPharma.Web.Models;
using HazziPharma.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly HazziPharmaDbContext _context;

        public PurchaseController(HazziPharmaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create()
        {
            var model = new PurchaseViewModel();

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
                model.Suppliers = await _context.Suppliers
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    })
                    .ToListAsync();

                model.Items[0].Products = await _context.Products
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    })
                    .ToListAsync();

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
                TotalAmount = model.Items[0].SubTotal
            };

            _context.Purchases.Add(purchase);

            await _context.SaveChangesAsync();

            // Purchase Detail
            var detail = new PurchaseDetail
            {
                PurchaseId = purchase.Id,
                ProductId = model.Items[0].ProductId,
                PurchasePrice = model.Items[0].PurchasePrice,
                Quantity = model.Items[0].Quantity,
                BatchNo = model.Items[0].BatchNo,
                ExpiryDate = model.Items[0].ExpiryDate,
                SubTotal = model.Items[0].SubTotal
            };

            _context.PurchaseDetails.Add(detail);

            // Stock Update
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == model.Items[0].ProductId);

            if (product != null)
            {
                product.Stock += model.Items[0].Quantity;
            }

            await _context.SaveChangesAsync();

            return Content("Purchase Saved Successfully");
        }
    }
}