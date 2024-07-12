namespace PetClinicApiConsumer.Models;

public class AppSettings
{
    public Services Services { get; set; } = new();
}

public class Services
{
    public const string SectionName = "Services";
    public PetClinic PetClinic { get; set; } = new();
}

public class PetClinic
{
    public const string SectionName = "PetClinic";
    public string BaseUrl { get; set; } = string.Empty;
    public string Vets { get; set; } = string.Empty;
    public string Owners { get; set; } = string.Empty;
}