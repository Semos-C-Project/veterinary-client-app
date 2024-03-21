using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using veterinary_client_app.Data;
using veterinary_client_app.Models.Entities;

namespace veterinary_client_app.Controllers;

[Authorize]
public class PetController : Controller
{
    private readonly AppDbContext _dbContext;

    public PetController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: pets
    public async Task<IActionResult> Index()
    {
        var pets = await _dbContext.Pets.Include(p => p.Owner).ToListAsync();
        return View(pets);
    }
    
    // GET: Pet/Details/5
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null) return BadRequest("Pet Id is missing");

        var pet = await _dbContext.Pets.Include(p=>p.Owner).Include(p=>p.Vaccines).FirstOrDefaultAsync(m => m.PetId == id);
        if (pet == null)
        {
            return NotFound($"Pet with id {id} was not found");
        }

        return View(pet);
    }
    
    // GET: Pet/Create
    public IActionResult Create()
    {
        ViewData["OwnerId"] = new SelectList(_dbContext.Owners, "OwnerId", "Name");
        ViewData["VaccineIds"] = new SelectList(_dbContext.Vaccines, "VaccineId", "Name");

        return View();
    }
    
    // POST: Pet/Create
    [HttpPost]
    public async Task<IActionResult> Create([Bind("PetId,Name,Type,Age,OwnerId")] Pet pet, long[] SelectedVaccineIds)
    {
        if (SelectedVaccineIds != null)
        {
            pet.Vaccines = new List<Vaccine>();
            foreach (var vaccine in SelectedVaccineIds)
            {
                var vaccineToAdd = await _dbContext.Vaccines.FindAsync(vaccine);
                if (vaccineToAdd != null)
                {
                    pet.Vaccines.Add(vaccineToAdd);
                }
            }
        }
        if (ModelState.IsValid)
        {
            await _dbContext.AddAsync(pet);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["OwnerId"] = new SelectList(_dbContext.Owners, "OwnerId", "Name", pet.OwnerId);
        ViewData["VaccineIds"] = new SelectList(_dbContext.Vaccines, "VaccineId", "Name");
        
        return View();
    }
    
    // GET: Pet/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null) return BadRequest();

        var pet = await _dbContext.Pets.Include(p => p.Vaccines).FirstOrDefaultAsync(p => p.PetId == id);
        if (pet == null)
        {
            return NotFound($"Pet with id {id} was not found");
        }

        ViewData["OwnerId"] = new SelectList(_dbContext.Owners, "OwnerId", "Name", pet.OwnerId);
        
        var selectedVaccines = pet.Vaccines.Select(v => v.VaccineId).ToList();
        ViewData["VaccineIds"] = new SelectList(_dbContext.Vaccines, "VaccineId", "Name", selectedVaccines);

        return View(pet);
    }
    
    // POST: Pet/Edit/5
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(long id, [Bind("PetId,Name,Type,Age, OwnerId")] Pet pet, long[] SelectedVaccineIds)
    {
        if (id != pet.PetId) return BadRequest();

        var petToUpdate = await _dbContext.Pets.Include(p => p.Vaccines).FirstOrDefaultAsync(p => p.PetId == id);

        if (petToUpdate == null) return NotFound();

        if (SelectedVaccineIds != null)
        {
            petToUpdate.Vaccines = new List<Vaccine>();
            foreach (var vaccine in SelectedVaccineIds)
            {
                var vaccineToAdd = await _dbContext.Vaccines.FindAsync(vaccine);
                if (vaccineToAdd != null)
                {
                    petToUpdate.Vaccines.Add(vaccineToAdd);
                }
            }
        }
        else
        {
            petToUpdate.Vaccines.Clear();
        }

        if (await TryUpdateModelAsync<Pet>(
                petToUpdate,
                "",
                p => p.Name, p => p.Type, p => p.Age, p => p.OwnerId))
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(pet.PetId))
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

        ViewData["OwnerId"] = new SelectList(_dbContext.Owners, "OwnerId", "Name", pet.OwnerId);
        ViewData["VaccineIds"] = new SelectList(_dbContext.Vaccines, "VaccineId", "Name", SelectedVaccineIds);
        
        return View(pet);
    }

    // GET: Pet/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null) return BadRequest();

        var pet = await _dbContext.Pets.Include(p=>p.Owner).FirstOrDefaultAsync(p=>p.PetId == id);
        if (pet == null)
        {
            return NotFound();
        }

        return View(pet);
    }
    
    // POST: Pet/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var pet = await _dbContext.Pets.FindAsync(id);
        if (pet == null) return NotFound();
        
        _dbContext.Pets.Remove(pet);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    // Method to check if an owner exists in some cases, not necessary but experimental for learning Exceptions
    private bool PetExists(long id)
    {
        return _dbContext.Pets.Any(o => o.OwnerId == id);
    }
}