using HazziPharma.Web.Data;
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