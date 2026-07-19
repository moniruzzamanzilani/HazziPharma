using HazziPharma.Web.Data;
using HazziPharma.Web.Models;
using HazziPharma.Web.Services;
using HazziPharma.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Controllers
{
    [Authorize]
    public class PurchaseReturnsController : Controller
    {
        private readonly HazziPharmaDbContext _context;
        private readonly NumberGeneratorService _numberGenerator;

        public PurchaseReturnsController(
            HazziPharmaDbContext context,
            NumberGeneratorService numberGenerator)
        {
            _context = context;
            _numberGenerator = numberGenerator;
        }

        // =========================
        // GET: PurchaseReturns/Create
        // =========================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PurchaseReturnViewModel();

            model.ReturnNo = await _numberGenerator.GeneratePurchaseReturnNoAsync();

            model.Purchases = await _context.Purchases
                .OrderByDescending(p => p.Id)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.PurchaseNo
                })
                .ToListAsync();

            model.Items.Add(new PurchaseReturnDetailViewModel
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var returns = await _context.PurchaseReturns
                .Include(x => x.Purchase)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return View(returns);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var purchaseReturn = await _context.PurchaseReturns
                .Include(x => x.Purchase)
                .Include(x => x.PurchaseReturnDetails)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (purchaseReturn == null)
            {
                return NotFound();
            }

            return View(purchaseReturn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseReturnViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Purchases = await _context.Purchases
                    .OrderByDescending(p => p.Id)
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.PurchaseNo
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

                return View(model);
            }

            var purchaseReturn = new PurchaseReturn
            {
                ReturnNo = model.ReturnNo,
                PurchaseId = model.PurchaseId,
                ReturnDate = model.ReturnDate,
                Remarks = model.Remarks,
                TotalAmount = model.TotalAmount
            };

            _context.PurchaseReturns.Add(purchaseReturn);

            await _context.SaveChangesAsync();
            foreach (var item in model.Items)
            {
                var detail = new PurchaseReturnDetail
                {
                    PurchaseReturnId = purchaseReturn.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ReturnPrice = item.ReturnPrice,
                    SubTotal = item.SubTotal
                };

                _context.PurchaseReturnDetails.Add(detail);

                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product != null)
                {
                    product.Stock -= item.Quantity;
                }
            }
           

            await _context.SaveChangesAsync();

            return Content("Purchase Return Header Saved");
        }
        // =========================
        // Get Products By Purchase
        // =========================
        [HttpGet]
        public async Task<JsonResult> GetPurchaseProducts(int purchaseId)
        {
            var products = await _context.PurchaseDetails
                .Where(x => x.PurchaseId == purchaseId)
               .Select(x => new
               {
                   productId = x.ProductId,
                   productName = x.Product.Name,
                   purchasePrice = x.PurchasePrice,
                   batchNo = x.BatchNo,
                   expiryDate = x.ExpiryDate
               })
                .ToListAsync();

            return Json(products);
        }
    }

}