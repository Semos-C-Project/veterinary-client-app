using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using veterinary_client_app.Models.Entities;

namespace veterinary_client_app.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
}