namespace PetClinicApiConsumer.Models;

public class AppSettings
{
    public Services Services { get; set; } = new();
}

public class Services
{
    public const string SectionName = "Services";
    public string PetClinicBaseApiUrl { get; set; } = string.Empty;
}