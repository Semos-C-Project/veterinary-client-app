using System.ComponentModel.DataAnnotations;

namespace veterinary_client_app.Models.Entities;

public class Owner
{
    public long OwnerId { get; init; }
    
    [Required]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; init; }
    
    [Required]
    [StringLength(100, ErrorMessage = "Surname cannot be longer than 100 characters.")]
    public string Surname { get; set; }
    
    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100 years!")]
    public int Age { get; init; }

    public ICollection<Pet> Pets { get; init; } = new List<Pet>();
}