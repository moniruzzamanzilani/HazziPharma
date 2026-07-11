using HazziPharma.Web.Data;
using HazziPharma.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Controllers
{
    public class CompanyProfileController : Controller
    {
        private readonly HazziPharmaDbContext _context;

        public CompanyProfileController(HazziPharmaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var company = await _context.CompanyProfiles.FirstOrDefaultAsync();

            if (company == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View(company);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _context.CompanyProfiles.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyProfile model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.CompanyProfiles.Update(model);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyProfile model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.CompanyProfiles.Add(model);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}