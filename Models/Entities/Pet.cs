using System.ComponentModel.DataAnnotations;

namespace veterinary_client_app.Models.Entities;

public class Pet
{
    public long PetId { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required string Type { get; set; }
    
    [Range(0, 50, ErrorMessage = "Age must be between 0 and 50 years!")]
    public int Age { get; set; }
    
    public long OwnerId { get; set; }
    public Owner? Owner { get; set; }

    public ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}