namespace PetClinicApiConsumer.Models;

public class VetSpeciality
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Vet
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<VetSpeciality> specialties { get; set; } = [];
}
