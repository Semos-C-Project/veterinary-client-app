using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veterinary_client_app.Data;
using veterinary_client_app.Models.Entities;

namespace veterinary_client_app.Controllers;

public class OwnerController : Controller
{
    private readonly AppDbContext _dbContext;

    public OwnerController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: owner
    public async Task<IActionResult> Index()
    {
        var owners =await _dbContext.Owners.ToListAsync();
        return View(owners);
    }
    
    // GET: Owner/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null) return BadRequest("Owner Id is missing");

        var owner = await _dbContext.Owners.FirstOrDefaultAsync(m => m.OwnerId == id);
        if (owner == null)
        {
            return NotFound($"Owner with id {id} was not found");
        }

        return View(owner);
    }
    
    // GET: Owner/Create
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: Owner/Create
    [HttpPost]
    public async Task<IActionResult> Create([Bind("OwnerId,Name,Age")] Owner owner)
    {
        if (ModelState.IsValid)
        {
            await _dbContext.AddAsync(owner);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View();
    }
    
    // GET: Owner/Edit/5
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null) return BadRequest();

        var owner = await _dbContext.Owners.FindAsync(id);
        if (owner == null)
        {
            return NotFound($"Owner with id {id} was not found");
        }

        return View(owner);
    }
    
    // POST: Owner/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(long id, [Bind("OwnerId,Name,Age")] Owner owner)
    {
        if (id != owner.OwnerId) return BadRequest();

        if (ModelState.IsValid)
        {
            try
            {
                _dbContext.Update(owner);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(owner.OwnerId))
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

        return View(owner);
    }

    // GET: Owner/Delete/5
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null) return BadRequest();

        var owner = await _dbContext.Owners.FindAsync(id);
        if (owner == null)
        {
            return NotFound();
        }

        return View(owner);
    }
    
    // POST: Owner/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var owner = await _dbContext.Owners.FindAsync(id);
        if (owner == null) return NotFound();
        
        _dbContext.Owners.Remove(owner);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    // Method to check if an owner exists in some cases, not necessary but experimental for learning Exceptions
    private bool OwnerExists(long id)
    {
        return _dbContext.Owners.Any(o => o.OwnerId == id);
    }
}