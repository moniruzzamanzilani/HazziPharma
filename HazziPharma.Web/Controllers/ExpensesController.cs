
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HazziPharma.Web.Models;
using HazziPharma.Web.Data;

public class ExpensesController : Controller
{
    private readonly HazziPharmaDbContext _context;

    public ExpensesController(HazziPharmaDbContext context)
    {
        _context = context;
    }

    // GET: EXPENSES
    public async Task<IActionResult> Index()
    {
        var expenses = await _context.Expenses
            .Include(x => x.ExpenseCategory)
            .OrderByDescending(x => x.ExpenseDate)
            .ToListAsync();

        return View(expenses);
    }

    // GET: EXPENSES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var expense = await _context.Expenses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (expense == null)
        {
            return NotFound();
        }

        return View(expense);
    }

    // GET: EXPENSES/Create
    public IActionResult Create()
    {
        ViewBag.ExpenseCategories = _context.ExpenseCategories
            .OrderBy(x => x.Name)
            .ToList();

        return View(new Expense
        {
            ExpenseDate = DateTime.Today
        });
    }
    // POST: EXPENSES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Expense expense)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ExpenseCategories = _context.ExpenseCategories
                .OrderBy(x => x.Name)
                .ToList();

            return View(expense);
        }

        _context.Expenses.Add(expense);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: EXPENSES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
        {
            return NotFound();
        }

        ViewBag.ExpenseCategories = await _context.ExpenseCategories
            .OrderBy(x => x.Name)
            .ToListAsync();

        return View(expense);
    }

    // POST: EXPENSES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,ExpenseDate,ExpenseCategoryId,ExpenseCategory,Amount,ReferenceNo,Description")] Expense expense)
    {
        if (id != expense.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(expense);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(expense.Id))
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
        return View(expense);
    }

    // GET: EXPENSES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var expense = await _context.Expenses
    .Include(x => x.ExpenseCategory)
    .FirstOrDefaultAsync(m => m.Id == id);
        if (expense == null)
        {
            return NotFound();
        }

        return View(expense);
    }

    // POST: EXPENSES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ExpenseExists(int? id)
    {
        return _context.Expenses.Any(e => e.Id == id);
    }
}
