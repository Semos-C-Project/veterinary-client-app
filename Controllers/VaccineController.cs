using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veterinary_client_app.Data;
using veterinary_client_app.Models.Entities;

namespace veterinary_client_app.Controllers;

public class VaccineController : Controller
{
    private readonly AppDbContext _dbContext;

    public VaccineController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: Vaccine
    public async Task<IActionResult> Index()
    {
        return View(await _dbContext.Vaccines.ToListAsync());
    }

    // GET: Vaccine/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vaccine = await _dbContext.Vaccines
            .FirstOrDefaultAsync(m => m.VaccineId == id);
        if (vaccine == null)
        {
            return NotFound();
        }

        return View(vaccine);
    }

    // GET: Vaccine/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Vaccine/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VaccineId,Name")] Vaccine vaccine)
    {
        if (ModelState.IsValid)
        {
            await _dbContext.AddAsync(vaccine);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vaccine);
    }

    // GET: Vaccine/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vaccine = await _dbContext.Vaccines.FindAsync(id);
        if (vaccine == null)
        {
            return NotFound();
        }
        return View(vaccine);
    }

    // POST: Vaccine/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("VaccineId,Name")] Vaccine vaccine)
    {
        if (id != vaccine.VaccineId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _dbContext.Update(vaccine);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccineExists(vaccine.VaccineId))
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
        return View(vaccine);
    }

    // GET: Vaccine/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vaccine = await _dbContext.Vaccines
            .FirstOrDefaultAsync(m => m.VaccineId == id);
        if (vaccine == null)
        {
            return NotFound();
        }

        return View(vaccine);
    }

    // POST: Vaccine/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var vaccine = await _dbContext.Vaccines.FindAsync(id);
        _dbContext.Vaccines.Remove(vaccine);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VaccineExists(long id)
    {
        return _dbContext.Vaccines.Any(e => e.VaccineId == id);
    }
}