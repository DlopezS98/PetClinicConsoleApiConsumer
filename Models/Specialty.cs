namespace PetClinicApiConsumer.Models;

public class Specialty
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public override string ToString()
    {
        return "id: " + Id + " name " + Name;
    }
}
