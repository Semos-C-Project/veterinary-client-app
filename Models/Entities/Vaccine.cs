using System.ComponentModel.DataAnnotations;

namespace veterinary_client_app.Models.Entities;

public class Vaccine
{
    public long VaccineId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    public ICollection<Pet> Pets = new List<Pet>();
}