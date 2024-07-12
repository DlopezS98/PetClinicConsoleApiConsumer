namespace PetClinicApiConsumer.Models;

public class Visit
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int PetId { get; set; }
    public DateTime Date { get; set; }
    public override string ToString()
    {
        return "id: " + Id + " Description " + Description + " date " + Date + " petId " + PetId;
    }
}
