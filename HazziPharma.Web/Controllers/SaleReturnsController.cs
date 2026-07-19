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
    }
}