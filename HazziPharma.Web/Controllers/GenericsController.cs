using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HazziPharma.Web.Models;
using HazziPharma.Web.Data;


    [Authorize]
    public class GenericsController : Controller
    {
        private readonly HazziPharmaDbContext _context;

        public GenericsController(HazziPharmaDbContext context)
        {
            _context = context;
        }

        // GET: GENERICS
        public async Task<IActionResult> Index()
        {
            return View(await _context.Generics.ToListAsync());
        }

        // GET: GENERICS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generic = await _context.Generics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generic == null)
            {
                return NotFound();
            }

            return View(generic);
        }

        // GET: GENERICS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GENERICS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Generic generic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generic);
        }

        // GET: GENERICS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generic = await _context.Generics.FindAsync(id);
            if (generic == null)
            {
                return NotFound();
            }
            return View(generic);
        }

        // POST: GENERICS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] Generic generic)
        {
            if (id != generic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenericExists(generic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(generic);
        }

        // GET: GENERICS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generic = await _context.Generics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generic == null)
            {
                return NotFound();
            }

            return View(generic);
        }

        // POST: GENERICS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var generic = await _context.Generics.FindAsync(id);
            if (generic != null)
            {
                _context.Generics.Remove(generic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenericExists(int? id)
        {
            return _context.Generics.Any(e => e.Id == id);
        }
    }
