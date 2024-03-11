namespace veterinary_client_app.Models.Entities;

public class Pet
{
    public long PetId { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public int Age { get; set; }
    public long OwnerId { get; set; }
    public Owner? Owner { get; set; }
}