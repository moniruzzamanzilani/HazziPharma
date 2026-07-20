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
    public class SaleReturnsController : Controller
    {
        private readonly HazziPharmaDbContext _context;
        private readonly NumberGeneratorService _numberGenerator;

        public SaleReturnsController(
            HazziPharmaDbContext context,
            NumberGeneratorService numberGenerator)
        {
            _context = context;
            _numberGenerator = numberGenerator;
        }
    
    [HttpGet]
        public async Task<IActionResult> Index()
        {
            var saleReturns = await _context.SaleReturns
                .Include(x => x.Sale)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return View(saleReturns);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SaleReturnViewModel();

            model.ReturnNo = await _numberGenerator.GenerateSaleReturnNoAsync();

            model.Sales = await _context.Sales
                .OrderByDescending(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.SaleNo
                })
                .ToListAsync();

            model.Items.Add(new SaleReturnDetailViewModel
            {
                Products = new List<SelectListItem>()
            });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleReturnViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.ReturnNo = await _numberGenerator.GenerateSaleReturnNoAsync();

                model.Sales = await _context.Sales
                    .OrderByDescending(x => x.Id)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.SaleNo
                    })
                    .ToListAsync();

                return View(model);
            }

            var saleReturn = new SaleReturn
            {
                ReturnNo = model.ReturnNo,
                SaleId = model.SaleId,
                ReturnDate = model.ReturnDate,
                Remarks = model.Remarks,
                TotalAmount = model.TotalAmount
            };

            _context.SaleReturns.Add(saleReturn);

            await _context.SaveChangesAsync();

            foreach (var item in model.Items)
            {
                if (item.Quantity <= 0)
                    continue;

                var detail = new SaleReturnDetail
                {
                    SaleReturnId = saleReturn.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ReturnPrice = item.ReturnPrice,
                    SubTotal = item.SubTotal
                };

                _context.SaleReturnDetails.Add(detail);
                var product = await _context.Products
                     .FirstOrDefaultAsync(x => x.Id == item.ProductId);

                if (product != null)
                {
                    product.Stock += item.Quantity;
                }
            }
            await _context.SaveChangesAsync();

            return Content("Sale Return Header Saved");
        }

        [HttpGet]
        public async Task<JsonResult> GetSaleItems(int saleId)
        {
            var items = await _context.SaleDetails
                .Where(x => x.SaleId == saleId)
                .Select(x => new
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product!.Name,
                    SalePrice = x.SalePrice,
                    Quantity = x.Quantity
                })
                .ToListAsync();

            return Json(items);
        }
    }
}