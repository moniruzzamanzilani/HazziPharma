using HazziPharma.Web.Data;
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
    }
}