
using HazziPharma.Web.Data;
using HazziPharma.Web.Models;
using HazziPharma.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace HazziPharma.Web.Controllers
{
    [Authorize]
    public class CompanyProfileController : Controller
    {
        private readonly HazziPharmaDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CompanyProfileController(
            HazziPharmaDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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

            var model = new CompanyProfileViewModel
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                Address = company.Address,
                Phone = company.Phone,
                Email = company.Email,
                Website = company.Website,
                TradeLicense = company.TradeLicense,
                BIN = company.BIN,
                LogoPath = company.LogoPath
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyProfileViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var company = await _context.CompanyProfiles.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            company.CompanyName = model.CompanyName;
            company.Address = model.Address;
            company.Phone = model.Phone;
            company.Email = model.Email;
            company.Website = model.Website;
            company.TradeLicense = model.TradeLicense;
            company.BIN = model.BIN;

            // Upload New Logo
            if (model.LogoFile != null && model.LogoFile.Length > 0)
            {
                // Delete Old Logo
                if (!string.IsNullOrEmpty(company.LogoPath))
                {
                    string oldFile = Path.Combine(
                        _environment.WebRootPath,
                        company.LogoPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                // Save New Logo
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images", "logo");

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.LogoFile.FileName);

                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(stream);
                }

                company.LogoPath = "/images/logo/" + fileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CompanyProfileViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? logoPath = null;

            if (model.LogoFile != null && model.LogoFile.Length > 0)
            {
                // Create Folder Path
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images", "logo");

                // Create Unique File Name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.LogoFile.FileName);

                string filePath = Path.Combine(uploadFolder, fileName);

                // Save Image
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(stream);
                }

                logoPath = "/images/logo/" + fileName;
            }

            var company = new CompanyProfile
            {
                CompanyName = model.CompanyName,
                Address = model.Address,
                Phone = model.Phone,
                Email = model.Email,
                Website = model.Website,
                TradeLicense = model.TradeLicense,
                BIN = model.BIN,
                LogoPath = logoPath
            };

            _context.CompanyProfiles.Add(company);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}