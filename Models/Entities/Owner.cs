using System.ComponentModel.DataAnnotations;

namespace veterinary_client_app.Models.Entities;

public class Owner
{
    public long OwnerId { get; set; }
    
    [Required]
    public string Name { get; set; }
    public int Age { get; set; }
    public List<Pet>? Pets { get; set; }
}